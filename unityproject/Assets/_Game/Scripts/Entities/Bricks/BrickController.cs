using UnityEngine;
using Zenject;

public class BrickController : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private int _scoreValue = 100;
    [SerializeField] private string _brickId = "standard_brick";

    private SignalBus _signalBus;
    private ILogger _logger;

    [Inject]
    public void Construct(SignalBus signalBus, ILogger logger)
    {
        _signalBus = signalBus;
        _logger = logger;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if hit by ball
        if (collision.gameObject.GetComponent<BallController>() != null)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        
        if (_health <= 0)
        {
            DestroyBrick();
        }
    }

    private void DestroyBrick()
    {
        if (_signalBus != null)
        {
            _signalBus.Fire(new BrickDestroyedSignal 
            { 
                BrickId = _brickId, 
                ScoreValue = _scoreValue 
            });
        }

        _logger?.LogInfo($"Brick destroyed: {_brickId}");
        
        Destroy(gameObject);
    }
}
