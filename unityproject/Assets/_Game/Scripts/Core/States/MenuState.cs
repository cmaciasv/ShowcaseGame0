public class MenuState : BaseGameState
{
    public MenuState(ILogger logger) : base(logger) { }

    public override void Enter()
    {
        Logger.LogInfo("Entered Menu State");
    }

    public override void Update()
    {
        // Handle menu-specific input/logic
    }
}
