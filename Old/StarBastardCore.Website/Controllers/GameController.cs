﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using StarBastardCore.Website.Code.DataAccess;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Gameplay.Actions;
using StarBastardCore.Website.Code.Game.Gameworld.Geography.Buildings;
using StarBastardCore.Website.Code.ModelBinding;
using StarBastardCore.Website.Models.Game;
using StarBastardCore.Website.SignalrHub;
using WebMatrix.WebData;

namespace StarBastardCore.Website.Controllers
{
    public class GameController : Controller
    {
        private readonly Storage _gameStorage;

        public GameController(Storage gameStorage)
        {
            _gameStorage = gameStorage;
        }

        [Authorize]
        public ActionResult View(Guid id, bool fogOfWar = true)
        {
            var game = _gameStorage.Load<GameContext>(id);
            var loggedInPlayer = game.Players.Single(x => x.UserId == WebSecurity.CurrentUserId);
            var gameboardViewModel = SinglePlayersViewOfTheGameboardViewModel.FromGameContext(game, loggedInPlayer, fogOfWar);

            var vm = new GameBoardAndSupportingUiDataViewModel(gameboardViewModel)
                {
                    AvailableBuildingTypes = GetAvailableBuildings(),
                    LoggedInPlayer = game.Players.Single(x => x.UserId == WebSecurity.CurrentUserId)
                };

            return View(vm);
        }

        [Authorize]
        public ActionResult QueueAction(Guid id, [FromJson] GameActionBase action)
        {
            var game = _gameStorage.Load<GameContext>(id);

            if (game.CurrentPlayer.UserId != WebSecurity.CurrentUserId)
            {
                return new HttpUnauthorizedResult("Incorrect player.");
            }

            var ns = action.GetType().Namespace;
            var strongType = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Namespace == ns).SingleOrDefault(x => x.Name == action.ActionName);
            var typedInstance = (GameActionBase)Activator.CreateInstance(strongType);
            typedInstance.Parameters = action.Parameters;

            game.UncommittedActions.Add(typedInstance);

            return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    ContentType = "application/json",
                    Data = game.UncommittedActions,
                };
        }

        [Authorize]
        [HttpPost]
        public ActionResult EndTurn(Guid id)
        {
            var game = _gameStorage.Load<GameContext>(id);

            if (game.CurrentPlayer.UserId == WebSecurity.CurrentUserId)
            {
                game.EndTurn();
            }

            var hub = new ClientPushHub();
            hub.ForceClientRefresh(id, GlobalHost.ConnectionManager.GetHubContext<ClientPushHub>());

            return RedirectToAction("View", new { id = game.Id });
        }

        private static List<string> GetAvailableBuildings()
        {
            return GetItemsOfType(typeof (IBuilding));
        }

        private static List<string> GetItemsOfType(Type type)
        {
            return
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => x.GetInterfaces().Contains(type))
                    .Where(x => !x.Name.Contains("Base"))
                    .Select(t => t.Name).ToList();
        }

    }

    public class GameBoardAndSupportingUiDataViewModel
    {
        public SinglePlayersViewOfTheGameboardViewModel Gameboard { get; set; }
        public List<string> AvailableBuildingTypes { get; set; }
        public Player LoggedInPlayer { get; set; }

        public GameBoardAndSupportingUiDataViewModel(SinglePlayersViewOfTheGameboardViewModel gameboard)
        {
            Gameboard = gameboard;
        }
    }

    public static class WebSecurityEx
    {
        public static bool IsAuthenticated()
        {
            return !string.IsNullOrWhiteSpace(WebSecurity.CurrentUserName);
        }
    }
}
