using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StarBastardCore.Website.Code.DataAccess;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Gameplay.GameGeneration;
using WebMatrix.WebData;

namespace StarBastardCore.Website.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {       
        private readonly SystemGenerator _generator;
        private readonly GameRepository _gameRepository;

        public LobbyController(SystemGenerator generator, GameRepository gameRepository)
        {
            _generator = generator;
            _gameRepository = gameRepository;
        }

        public ActionResult Index()
        {
            return View();
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

            return RedirectToAction("View", "Game", new { id = game.Id });
        }
    }
}
