using UnityEngine;
using Zenject;

public interface IGameManager
{
    void StartGame();
    void WinGame();
    void LoseGame();
    void ChangeState(IGameState newState);
}

public class GameManager : IGameManager, IInitializable, ITickable
{    
    private IGameState _currentState;
    private readonly ILogger _logger;
    
    [Inject] public MenuState _menuState;
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
        ChangeState(_gameplayState);
    }

    public void WinGame()
    {
        _logger.LogInfo("Game Won!");
        ChangeState(_winState);
    }

    public void LoseGame()
    {
        _logger.LogInfo("Game Lost!");
        ChangeState(_loseState);
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


