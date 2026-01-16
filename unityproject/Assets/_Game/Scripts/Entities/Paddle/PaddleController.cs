using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _acceleration = 50f;
    [SerializeField] private float _maxSpeed = 15f;
    [SerializeField] private float _friction = 10f;
    
    [Header("Boundaries")]
    [SerializeField] private float _screenMargin = 0.5f;

    private Rigidbody2D _rb;
    private IInputReader _inputReader;
    private ILogger _logger;
    private Camera _mainCamera;
    private float _minX, _maxX;

    [Inject]
    public void Construct(IInputReader inputReader, ILogger logger)
    {
        _inputReader = inputReader;
        _logger = logger;
        
        _logger.LogInfo("PaddleController: InputReader and Logger injected successfully!");
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
        
        // Set kinematic to ensure vertical stability
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.useFullKinematicContacts = true;
        
        CalculateBoundaries();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        ClampPosition();
    }

    private void HandleMovement()
    {
        float input = _inputReader.MoveValue;
        Vector2 velocity = _rb.linearVelocity;

        // Apply acceleration based on input
        if (Mathf.Abs(input) > 0.01f)
        {
            velocity.x += input * _acceleration * Time.fixedDeltaTime;
        }
        else
        {
            // Apply friction when no input
            velocity.x = Mathf.MoveTowards(velocity.x, 0, _friction * Time.fixedDeltaTime);
        }

        // Clamp to max speed
        velocity.x = Mathf.Clamp(velocity.x, -_maxSpeed, _maxSpeed);
        
        _rb.linearVelocity = velocity;
    }

    private void ClampPosition()
    {
        Vector3 pos = transform.position;
        
        if (pos.x < _minX)
        {
            pos.x = _minX;
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
        }
        else if (pos.x > _maxX)
        {
            pos.x = _maxX;
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
        }
        
        transform.position = pos;
    }

    private void CalculateBoundaries()
    {
        if (_mainCamera == null) _mainCamera = Camera.main;
        
        float screenAspect = (float)Screen.width / Screen.height;
        float camHeight = _mainCamera.orthographicSize;
        float camWidth = camHeight * screenAspect;
        
        // Assuming paddle width of ~2 units (1 unit half-extent)
        float paddleHalfWidth = 1f; 
        
        _minX = -camWidth + paddleHalfWidth + _screenMargin;
        _maxX = camWidth - paddleHalfWidth - _screenMargin;
    }

    // Context menu to refresh boundaries if camera changes (development aid)
    [ContextMenu("Refresh Boundaries")]
    private void RefreshBoundaries() => CalculateBoundaries();
}
