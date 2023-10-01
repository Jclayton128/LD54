using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public static ProjectileController Instance { get; private set; }

    //settings
    [SerializeField] ProjectileHandler _bulletPrefab = null;


    //state
    Queue<ProjectileHandler> _pooledProjectiles = new Queue<ProjectileHandler>();
    List<ProjectileHandler> _activeProjectiles = new List<ProjectileHandler>();


    private void Awake()
    {
        Instance = this;
    }

    public void SpawnProjectile(Transform muzzle, int damage, float speed, float lifetime)
    {
        ProjectileHandler ph;
        if (_pooledProjectiles.Count > 0)
        {
            ph = _pooledProjectiles.Dequeue();
            ph.gameObject.SetActive(true);
        }
        else
        {
            ph = Instantiate(_bulletPrefab);
            ph.Initialize();
        }
        _activeProjectiles.Add(ph);
        ph.transform.SetPositionAndRotation(muzzle.position, muzzle.rotation);
        ph.Activate(damage, speed, lifetime);
    }

    public void DespawnProjectile(ProjectileHandler ph)
    {
        if (_activeProjectiles.Contains(ph)) _activeProjectiles.Remove(ph);
        ph.gameObject.SetActive(false);
        _pooledProjectiles.Enqueue(ph);
    }
}

