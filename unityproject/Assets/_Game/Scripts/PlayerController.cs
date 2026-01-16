using UnityEngine;
using Zenject;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    [Inject]
    private IGameManager _gameManager;
    
    [Inject]
    private ILogger _logger;

    void Awake()
    {
        // This method is called when the script instance is being loaded
        Debug.Log("Cube script has been loaded.");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         _gameManager.StartGame();
        _logger.LogInfo("Player Controller initialized.");
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal and vertical input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal"); // Left/Right Arrow Keys
        float verticalInput = Input.GetAxis("Vertical");   // Up/Down Arrow Keys

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // Apply movement to the camera's position
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
