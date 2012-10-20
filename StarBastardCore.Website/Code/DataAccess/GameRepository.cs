using System;
using System.Web;
using StarBastardCore.Website.Code.Game.Gameplay;

namespace StarBastardCore.Website.Code.DataAccess
{
    public class GameRepository
    {
        private readonly HttpContextBase _ctx;

        public GameRepository(HttpContextBase ctx)
        {
            _ctx = ctx;
            if(_ctx.Session == null)
            {
                throw new InvalidOperationException("No session storage, can't save games.");
            }
        }

        public GameContext Load(Guid gameId)
        {
            return (GameContext) _ctx.Session["game_" + gameId];
        }

        public GameContext Save(GameContext game)
        {
            _ctx.Session["game_" + game.Id] = game;
            return game;
        }
    }
}