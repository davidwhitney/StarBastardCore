namespace StarBastardCore.Website.Code.Game.Gameplay.Actions
{
    public interface IAction
    {
        void Execute(Player currentPlayer, GameContext context);
    }
}