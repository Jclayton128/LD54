using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public static AsteroidController Instance { get; private set; }

    //settings
    [SerializeField] float _spawnRadius = 10f;
    [SerializeField] AsteroidHandler _asteroidPrefab = null;
    [SerializeField] float _speed = 2f;
    [SerializeField] int _minSize = 1;
    [SerializeField] int _maxSize = 5;

    //state
    Queue<AsteroidHandler> _pooledAsteroids = new Queue<AsteroidHandler>();
    List<AsteroidHandler> _activeAsteroids = new List<AsteroidHandler>();

    private void Awake()
    {
        Instance = this;
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
        ah.transform.position = startPos;
        int rand = UnityEngine.Random.Range(_minSize, _maxSize + 1);
        ah.Activate(_speed/(float)rand, rand);

    }

    public void DespawnAsteroid(AsteroidHandler ah)
    {
        ah.gameObject.SetActive(false);
        _pooledAsteroids.Enqueue(ah);
    }
}
