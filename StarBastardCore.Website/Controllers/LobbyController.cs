using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StarBastardCore.Website.Code.DataAccess;
using StarBastardCore.Website.Code.Game.Gameplay;
using StarBastardCore.Website.Code.Game.Gameplay.GameGeneration;
using StarBastardCore.Website.Models.Lobby;
using WebMatrix.WebData;

namespace StarBastardCore.Website.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
        private readonly IDb _db;
        private readonly SystemGenerator _generator;
        private readonly Storage _gameStorage;

        public LobbyController(IDb db, SystemGenerator generator, Storage gameStorage)
        {
            _db = db;
            _generator = generator;
            _gameStorage = gameStorage;
        }

        public ActionResult Index()
        {
            return View();
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

            game.AddPlayer(new Player(WebSecurity.CurrentUserId, WebSecurity.CurrentUserName));
            game.AddPlayer(new Player(opponentId, opponent.UserName.ToString()));

            _gameStorage.Save(game);

            return RedirectToAction("View", "Game", new { id = game.Id });
        }
    }
}