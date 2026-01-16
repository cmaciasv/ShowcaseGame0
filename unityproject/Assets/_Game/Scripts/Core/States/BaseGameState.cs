using UnityEngine;
using Zenject;

public abstract class BaseGameState : IGameState
{
    protected readonly ILogger Logger;
    
    [Inject]
    protected readonly IGameManager GameManager;

    protected BaseGameState(ILogger logger)
    {
        Logger = logger;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
