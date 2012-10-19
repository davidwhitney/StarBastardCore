using System;
using System.Web.Mvc;

namespace StarBastardCore.Website.Controllers
{
    public class GameController : Controller
    {
        public RedirectToRouteResult Create()
        {
            var uniqueGameId = Guid.NewGuid();
            return RedirectToAction("View", new {id = uniqueGameId});
        }

        public ActionResult View(Guid id)
        {
            return View();
        }
    }
}
