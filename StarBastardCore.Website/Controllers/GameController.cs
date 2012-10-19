using System;
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

            var game = _generator.Generate();

            return RedirectToAction("View", new {id = uniqueGameId});
        }

        public ActionResult View(Guid id)
        {
            return View();
        }
    }
}
