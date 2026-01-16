public class GameplayState : BaseGameState
{
    public GameplayState(ILogger logger) : base(logger) { }

    public override void Enter()
    {
        Logger.LogInfo("Entered Gameplay State");
    }

    public override void Update()
    {
        // Core gameplay tick
    }
}
