using System;
using System.Web.Mvc;
using StarBastardCore.Website.Code.DataAccess;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Gameplay.GameGeneration;
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

            return View(vm);
        }
    }

    public class GameBoardAndSupportingUiDataViewModel
    {
        public SinglePlayersViewOfTheGameboardViewModel Gameboard { get; set; }

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
