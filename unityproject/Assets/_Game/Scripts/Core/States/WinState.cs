public class WinState : BaseGameState
{
    public WinState(ILogger logger) : base(logger) { }

    public override void Enter()
    {
        Logger.LogInfo("Entered Win State - VICTORY!");
    }
}
