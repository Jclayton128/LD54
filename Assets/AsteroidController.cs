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
    [SerializeField] int _maxSize_initial = 5;
    [SerializeField] float _averageTimeBetweenSpawns = 5;

    //state
    int _maxSize_current;
    Queue<AsteroidHandler> _pooledAsteroids = new Queue<AsteroidHandler>();
    List<AsteroidHandler> _activeAsteroids = new List<AsteroidHandler>();
    float _timeForNextAsteroidSpawn = 0;
    [SerializeField] bool _isSpawning = false;
    public bool HasActiveAsteroids { get; private set; } = false;
    [SerializeField] int _asteroidsToSpawn;
    public int RocksKilled;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _isSpawning = false;
        GameController.Instance.EnteredAttackMode += HandleEnterAttackMode;
        GameController.Instance.ExitedAttackMode += HandleExitAttackMode;
        GameController.Instance.EnterGameMode += HandleStartGame;
    }

    private void HandleStartGame()
    {
        _maxSize_current = _maxSize_initial;
        RocksKilled = 0;
    }

    private void HandleEnterAttackMode()
    {
        _asteroidsToSpawn = GameController.Instance.Difficulty * 2;
        _maxSize_current = _maxSize_initial + GameController.Instance.Difficulty;
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
            _asteroidsToSpawn--;
            if (_asteroidsToSpawn <= 0) _isSpawning = false;
            float rand = UnityEngine.Random.Range(0.5f, 1.5f);
            _timeForNextAsteroidSpawn = Time.time + (_averageTimeBetweenSpawns * rand);
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
        int rand = UnityEngine.Random.Range(_minSize, _maxSize_current);
        ah.Activate(_speed, rand);

    }

    public void StopSpawningAsteroids()
    {
        //for(int i = _activeAsteroids.Count -1; i >= 0; i--)
        //{
        //    _activeAsteroids[i].ExecuteDeath();
        //}
        _isSpawning = false;
        _asteroidsToSpawn = 0;
    }

    public void DespawnAsteroid(AsteroidHandler ah)
    {
        if (_activeAsteroids.Contains(ah)) _activeAsteroids.Remove(ah);

        //Debug.Log($"{_activeAsteroids.Count} alive, {_asteroidsToSpawn} left to spawn");
        if (_activeAsteroids.Count <= 0 && _asteroidsToSpawn <= 0)
        {
            //Debug.Log("last asteroid killed");
            LastAsteroidKilled?.Invoke();
            HasActiveAsteroids = false;
        }
        RocksKilled++;
        ah.gameObject.SetActive(false);
        _pooledAsteroids.Enqueue(ah);
    }
}
