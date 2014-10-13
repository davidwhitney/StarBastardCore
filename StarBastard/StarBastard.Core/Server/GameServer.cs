using System.Diagnostics;
using StarBastard.Core.Gameplay;
using StarBastard.Core.Universe.Systems;

namespace StarBastard.Core.Server
{
    public class GameServer
    {
        private GameContext _ctx;
        
        public void NewGame()
        {
            var gen = new SystemGenerator();
            var systems = gen.GenerateSystems();
            
            _ctx = new GameContext(systems, context =>
            {
                Debug.WriteLine("Render called due to change");
                OnChanged(new OnChangeArgs {CurrentState = systems});
            });

            _ctx.StartTurn();
        }

        public event OnChange Changed;

        protected virtual void OnChanged(OnChangeArgs args)
        {
            if (Changed != null)
            {
                Changed(this, args);
            }
        }
    }

    public delegate void OnChange(object sender, OnChangeArgs args);

    public class OnChangeArgs
    {
        public GameBoard CurrentState { get; set; }
    }
}
