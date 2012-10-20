using System;
using System.Web.Mvc;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Systems;
using System.Linq;
using StarBastardCore.Website.Models.Game;

namespace StarBastardCore.Website.Controllers
{
    public class GameController : Controller
    {
        private readonly SystemGenerator _generator;

        public GameController(SystemGenerator generator)
        {
            _generator = generator;
        }

        public RedirectToRouteResult Create()
        {
            var game = GameContext.Create(NamesRepository.RandomName())
                                    .WithSystems(_generator.Generate());

            game.AddPlayer(new Player("123", "david"));

            Session["game_" + game.Id] = game;

            return RedirectToAction("View", new { id = game .Id });
        }

        public ActionResult View(Guid id)
        {
            var game = (GameContext)Session["game_" + id];
            var vm = CurrentTurnViewModel.FromGameContext(game);
            return View(vm);
        }
    }
}
