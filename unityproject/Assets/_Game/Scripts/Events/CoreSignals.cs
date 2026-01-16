using Zenject;

public class GameStateChangedSignal
{
    public string NewState { get; set; }
}

public class BrickDestroyedSignal
{
    public string BrickId { get; set; }
    public int ScoreValue { get; set; }
}

public class BallCollisionSignal
{
    public float Velocity { get; set; }
}
