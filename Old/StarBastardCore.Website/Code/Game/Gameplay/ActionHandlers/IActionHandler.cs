namespace StarBastardCore.Website.Code.Game.Gameplay.ActionHandlers
{
    public interface IActionHandler
    {
        void Execute(Player currentPlayer, GameContext context);
    }
}