using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _columns = 10;
    [SerializeField] private Vector2 _spacing = new Vector2(1.1f, 0.6f);
    [SerializeField] private Vector2 _offset = new Vector2(-5f, 3f);

    private DiContainer _container;
    private ScoreManager _scoreManager;
    private List<GameObject> _spawnedBricks = new List<GameObject>();

    [Inject]
    public void Construct(DiContainer container, ScoreManager scoreManager)
    {
        _container = container;
        _scoreManager = scoreManager;
    }

    private void Start()
    {
        SpawnLevel();
    }

    [ContextMenu("Spawn Level")]
    public void SpawnLevel()
    {
        ClearLevel();

        if (_brickPrefab == null) return;

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _columns; c++)
            {
                Vector3 spawnPos = new Vector3(
                    _offset.x + (c * _spacing.x),
                    _offset.y - (r * _spacing.y),
                    0
                );

                GameObject brick;
                // Use Zenject Container in Play Mode, standard Instantiate in Editor (Edit Mode)
                if (_container != null)
                {
                    brick = _container.InstantiatePrefab(_brickPrefab, spawnPos, Quaternion.identity, transform);
                }
                else
                {
                    brick = Instantiate(_brickPrefab, spawnPos, Quaternion.identity, transform);
                }
                
                _spawnedBricks.Add(brick);
            }
        }

        if (_scoreManager != null)
        {
            _scoreManager.Reset(_spawnedBricks.Count);
        }
    }

    public void ClearLevel()
    {
        foreach (var brick in _spawnedBricks)
        {
            if (brick != null)
            {
                if (Application.isPlaying)
                    Destroy(brick);
                else
                    DestroyImmediate(brick);
            }
        }
        _spawnedBricks.Clear();
    }
}
