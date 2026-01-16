using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _initialSpeed = 10f;
    [SerializeField] private float _maxSpeed = 25f;
    [SerializeField] private float _speedRamp = 1.05f; // 5% increase per hit
    [SerializeField] private float _englishFactor = 0.2f;

    private Rigidbody2D _rb;
    private IInputReader _inputReader;
    private ILogger _logger;
    private bool _isLaunched = false;

    [Inject]
    public void Construct(IInputReader inputReader, ILogger logger)
    {
        _inputReader = inputReader;
        _logger = logger;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.gravityScale = 0f;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void OnEnable()
    {
        _inputReader.LaunchPerformed += HandleLaunch;
    }

    private void OnDisable()
    {
        if (_inputReader != null)
            _inputReader.LaunchPerformed -= HandleLaunch;
    }

    private void HandleLaunch()
    {
        if (_isLaunched) return;

        _isLaunched = true;
        // Launch upwards with a slight random variation
        Vector2 launchDir = new Vector2(Random.Range(-0.5f, 0.5f), 1f).normalized;
        _rb.linearVelocity = launchDir * _initialSpeed;
        
        _logger.LogInfo("Ball Launched!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isLaunched) return;

        // Apply "English" if we hit the paddle
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyEnglish(collision);
        }

        // Ramp speed
        Vector2 velocity = _rb.linearVelocity;
        float currentSpeed = velocity.magnitude;

        if (currentSpeed < _maxSpeed)
        {
            _rb.linearVelocity = velocity * _speedRamp;
        }
        
        // Ensure velocity doesn't drop too low on Y-axis (to avoid infinite horizontal bouncing)
        if (Mathf.Abs(_rb.linearVelocity.y) < 2f)
        {
            Vector2 v = _rb.linearVelocity;
            v.y = v.y > 0 ? 2f : -2f;
            _rb.linearVelocity = v.normalized * currentSpeed;
        }
    }

    private void ApplyEnglish(Collision2D collision)
    {
        // Get the paddle's velocity (from the Rigidbody contact)
        Rigidbody2D paddleRb = collision.rigidbody;
        if (paddleRb != null)
        {
            float paddleXVelocity = paddleRb.linearVelocity.x;
            Vector2 currentVelocity = _rb.linearVelocity;
            
            // Influence the X direction based on paddle movement
            currentVelocity.x += paddleXVelocity * _englishFactor;
            
            // Restore speed magnitude
            _rb.linearVelocity = currentVelocity.normalized * currentVelocity.magnitude;
        }
    }

    public void ResetBall(Vector3 position)
    {
        _isLaunched = false;
        _rb.linearVelocity = Vector2.zero;
        transform.position = position;
    }
}
