using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using StarBastardCore.Website.Code.DataAccess;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Gameplay.ActionHandlers;
using StarBastardCore.Website.Code.Game.Gameplay.Actions;
using StarBastardCore.Website.Code.Game.Gameplay.GameGeneration;
using StarBastardCore.Website.Code.Game.Gameworld.Geography.Buildings;
using StarBastardCore.Website.Code.ModelBinding;
using StarBastardCore.Website.Models.Game;
using WebMatrix.WebData;

namespace StarBastardCore.Website.Controllers
{
    public class GameController : Controller
    {
        private readonly SystemGenerator _generator;
        private readonly GameRepository _gameRepository;

        public GameController(SystemGenerator generator, GameRepository gameRepository)
        {
            _generator = generator;
            _gameRepository = gameRepository;
        }

        public RedirectToRouteResult Create()
        {
            var game = GameContext.Create(NamesRepository.RandomName())
                                    .WithSystems(_generator.Generate());

            game.AddPlayer(WebSecurityEx.IsAuthenticated()
                               ? new Player(WebSecurity.CurrentUserId, WebSecurity.CurrentUserName)
                               : Player.UnauthenticatedPlayer1);

            game.AddPlayer(Player.UnauthenticatedPlayer2);

            _gameRepository.Save(game);

            return RedirectToAction("View", new { id = game.Id });
        }

        public ActionResult View(Guid id, bool fogOfWar = true)
        {
            var game = _gameRepository.Load(id);
            var gameboardViewModel = SinglePlayersViewOfTheGameboardViewModel.FromGameContext(game, fogOfWar);

            var vm = new GameBoardAndSupportingUiDataViewModel(gameboardViewModel);
            vm.AvailableBuildingTypes = GetAvailableBuildings(); // Filter for current player for tech trees.

            return View(vm);
        }

        [Authorize]
        public ActionResult QueueAction(Guid id, [FromJson] GameActionBase action)
        {
            var game = _gameRepository.Load(id);

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
        public ActionResult EndTurn(Guid id)
        {
            var game = _gameRepository.Load(id);

            if (game.CurrentPlayer.UserId != WebSecurity.CurrentUserId)
            {
                return new HttpUnauthorizedResult("Incorrect player.");
            }

            game.EndTurn();

            return new JsonResult {Data = game};
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
