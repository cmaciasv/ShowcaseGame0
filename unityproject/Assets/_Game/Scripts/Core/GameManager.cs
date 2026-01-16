using UnityEngine;
using Zenject;

public interface IGameManager
{
    void StartGame();
    void ChangeState(IGameState newState);
}

public class GameManager : IGameManager, IInitializable, ITickable
{    
    private IGameState _currentState;
    private readonly ILogger _logger;
    
    [Inject] private MenuState _menuState;
    [Inject] private GameplayState _gameplayState;
    [Inject] private WinState _winState;
    [Inject] private LoseState _loseState;

    public GameManager(ILogger logger)
    {
        _logger = logger;
    }

    public void Initialize()
    {
        _logger.LogInfo("GameManager Initialized");
        ChangeState(_menuState);
    }

    public void Tick()
    {
        _currentState?.Update();
    }

    public void StartGame()
    {
        _logger.LogInfo("Starting Game...");
        // This will be called by UI or initial trigger
    }

    public void ChangeState(IGameState newState)
    {
        if (_currentState != null)
        {
            _logger.LogInfo($"Exiting State: {_currentState.GetType().Name}");
            _currentState.Exit();
        }

        _currentState = newState;

        if (_currentState != null)
        {
            _logger.LogInfo($"Entering State: {_currentState.GetType().Name}");
            _currentState.Enter();
        }
    }
}


