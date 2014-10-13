using System.Collections.Generic;

namespace StarBastard.Core.Gameplay
{
    public class Turn
    {
        private readonly Player _player;
        private List<object> _actionQueue;

        public Turn(Player player)
        {
            _player = player;
            _actionQueue = new List<object>();
        }

        public void tick(GameContext context)
        {
            var playerResourceChanges = new List<ResourceDelta>();

            foreach (var value in context.Systems)
            {
                if (value.Owner == _player.PlayerNumber)
                {
                    var resourceChanges = value.ProgressOneTurn();
                    foreach (var change in resourceChanges)
                    {
                        playerResourceChanges.Add(change);
                    }
                }
            }

            foreach (var change in playerResourceChanges)
            {
                _player.ModifyResources(change);
            }
        }



        public void commitTurn()
        {


        }
    }
}