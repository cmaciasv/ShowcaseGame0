using Zenject;
using System;

public class ScoreManager : IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly IGameManager _gameManager;
    private readonly ILogger _logger;

    private int _totalBricks;
    private int _destroyedBricks;
    private int _currentScore;

    public ScoreManager(SignalBus signalBus, IGameManager gameManager, ILogger logger)
    {
        _signalBus = signalBus;
        _gameManager = gameManager;
        _logger = logger;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<BrickDestroyedSignal>(OnBrickDestroyed);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<BrickDestroyedSignal>(OnBrickDestroyed);
    }

    public void Reset(int totalBricks)
    {
        _totalBricks = totalBricks;
        _destroyedBricks = 0;
        _currentScore = 0;
        _logger.LogInfo($"ScoreManager Reset: {_totalBricks} bricks to destroy.");
    }

    private void OnBrickDestroyed(BrickDestroyedSignal signal)
    {
        _destroyedBricks++;
        _currentScore += signal.ScoreValue;
        
        _logger.LogInfo($"Brick Destroyed! Score: {_currentScore}. Progress: {_destroyedBricks}/{_totalBricks}");

        if (_destroyedBricks >= _totalBricks && _totalBricks > 0)
        {
            _gameManager.WinGame();
        }
    }
}
