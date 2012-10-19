using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StarBastardCore.Website.Code.Game.Systems;

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
            var uniqueGameId = Guid.NewGuid();

            var systems = _generator.Generate();
            var game = new GameContext { Systems = systems };
            Session["game_" + uniqueGameId] = game;

            return RedirectToAction("View", new {id = uniqueGameId});
        }

        public ActionResult View(Guid id)
        {
            var game = Session["game_" + id] as GameContext;
            return View(game);
        }
    }

    public class GameContext
    {
        public List<PlanetarySystem> Systems { get; set; }

        public GameContext()
        {
            Systems = new List<PlanetarySystem>();
        }
    }
}
