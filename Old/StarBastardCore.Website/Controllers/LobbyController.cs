﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StarBastardCore.Website.Code.DataAccess;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Gameplay.GameGeneration;
using StarBastardCore.Website.Code.UserProfile;
using StarBastardCore.Website.Models.Lobby;
using WebMatrix.WebData;

namespace StarBastardCore.Website.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
        private readonly IDb _db;
        private readonly SystemGenerator _generator;
        private readonly Storage _storage;

        public LobbyController(IDb db, SystemGenerator generator, Storage storage)
        {
            _db = db;
            _generator = generator;
            _storage = storage;
        }

        public ActionResult Index()
        {
            var myProfile = _storage.GetOrEmpty<UserProfileData>(WebSecurity.CurrentUserId);
            return View(myProfile);
        }

        [HttpGet]
        public ActionResult Create(string searchQuery = null)
        {
            var potentialOpponents = new List<PotentialOpponent>();
            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                IEnumerable<PotentialOpponent> usersThatMatch =
                    _db.X.UserProfile.FindAll(_db.X.UserProfile.UserName.Like(searchQuery))
                                     .Cast<PotentialOpponent>();

                potentialOpponents.AddRange(usersThatMatch.ToList());
            }

            return View(potentialOpponents);
        }
        
        [HttpPost]
        public ActionResult SearchForPlayer(string name)
        {
            return RedirectToAction("Create", "Lobby", new {searchQuery = HttpUtility.UrlEncode(name)});
        }

        [HttpPost]
        public RedirectToRouteResult CreateGame(int opponentId)
        {
            var game = GameContext.Create(NamesRepository.RandomName())
                                    .WithSystems(_generator.Generate());

            var opponent = _db.X.UserProfile.FindByUserId(opponentId);
            var player1 = new Player(WebSecurity.CurrentUserId, WebSecurity.CurrentUserName);
            var player2 = new Player(opponentId, opponent.UserName.ToString());
            
            game.AddPlayer(player1);
            game.AddPlayer(player2);

            _storage.Save(game);

            var playerOneProfile = _storage.GetOrEmpty<UserProfileData>(WebSecurity.CurrentUserId);
            var playerTwoProfile = _storage.GetOrEmpty<UserProfileData>(opponentId);

            var activeGameReference = new ActiveGameReference
                {
                    Id = (Guid) game.Id,
                    Name = game.Name,
                    Players = new List<Player> { player1, player2 }
                };

            playerOneProfile.ActiveGames.Add(activeGameReference);
            playerTwoProfile.ActiveGames.Add(activeGameReference);

            _storage.Save(playerOneProfile);
            _storage.Save(playerTwoProfile);

            return RedirectToAction("View", "Game", new { id = game.Id });
        }
    }
}