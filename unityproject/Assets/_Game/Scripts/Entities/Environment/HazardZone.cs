using UnityEngine;
using Zenject;

public class HazardZone : MonoBehaviour
{
    private IGameManager _gameManager;

    [Inject]
    public void Construct(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the ball entered the hazard zone
        if (collision.GetComponent<BallController>() != null)
        {
            _gameManager.LoseGame();
        }
    }
}
