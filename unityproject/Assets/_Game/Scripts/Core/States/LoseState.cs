public class LoseState : BaseGameState
{
    public LoseState(ILogger logger) : base(logger) { }

    public override void Enter()
    {
        Logger.LogInfo("Entered Lose State - DEFEAT!");
    }
}
