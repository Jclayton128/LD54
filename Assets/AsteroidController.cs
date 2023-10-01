using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public Action LastAsteroidKilled;
    public static AsteroidController Instance { get; private set; }

    //settings
    [SerializeField] float _spawnRadius = 10f;
    [SerializeField] AsteroidHandler _asteroidPrefab = null;
    [SerializeField] float _speed = 2f;
    [SerializeField] int _minSize = 1;
    [SerializeField] int _maxSize = 5;
    [SerializeField] float _averageTimeBetweenSpawns = 5;

    //state
    Queue<AsteroidHandler> _pooledAsteroids = new Queue<AsteroidHandler>();
    List<AsteroidHandler> _activeAsteroids = new List<AsteroidHandler>();
    float _timeForNextAsteroidSpawn = 0;
    [SerializeField] bool _isSpawning = false;
    public bool HasActiveAsteroids { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _isSpawning = false;
        GameController.Instance.EnteredAttackMode += HandleEnterAttackMode;
        GameController.Instance.ExitedAttackMode += HandleExitAttackMode;
    }

    private void HandleEnterAttackMode()
    {
        _isSpawning = true;
        _timeForNextAsteroidSpawn = Time.time +
            (_averageTimeBetweenSpawns/GameController.Instance.Difficulty);
    }

    private void HandleExitAttackMode()
    {
        _isSpawning = false;
    }

    private void Update()
    {
        if (!_isSpawning) return;
        if (Time.time >= _timeForNextAsteroidSpawn)
        {
            SpawnNewAsteroid();
            _timeForNextAsteroidSpawn = Time.time +
                (_averageTimeBetweenSpawns / GameController.Instance.Difficulty);
        }
    }

    public void SpawnNewAsteroid()
    {
        Vector3 startPos = UnityEngine.Random.insideUnitCircle.normalized * _spawnRadius;
        AsteroidHandler ah;
        if (_pooledAsteroids.Count > 0)
        {
            ah = _pooledAsteroids.Dequeue();
            ah.gameObject.SetActive(true);
        }
        else
        {
            ah = Instantiate(_asteroidPrefab);
            ah.Initialize();
        }
        HasActiveAsteroids = true;
        _activeAsteroids.Add(ah);
        ah.transform.position = startPos;
        int rand = UnityEngine.Random.Range(_minSize, _maxSize + 1);
        ah.Activate(_speed, rand);

    }

    public void DespawnAsteroid(AsteroidHandler ah)
    {
        if (_activeAsteroids.Contains(ah)) _activeAsteroids.Remove(ah);
        if (_activeAsteroids.Count == 0)
        {
            LastAsteroidKilled?.Invoke();
            HasActiveAsteroids = false;
        }

        ah.gameObject.SetActive(false);
        _pooledAsteroids.Enqueue(ah);
    }
}
