using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHandler : MonoBehaviour
{
    SpriteRenderer _sr;
    Rigidbody2D _rb;

    //state
    int _healthRemaining;
     public bool IsDead;

    public void Initialize()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Activate(float speed, int health)
    {
        _healthRemaining = health;
        
        Vector3 dir = (Vector3.zero - transform.position).normalized;
        _rb.velocity = dir * speed;
        IsDead = false;
    }

    public void HandleStructureImpact()
    {
        _healthRemaining = 0;
        ExecuteDeath();
    }

    public void ExecuteDeath()
    {
        //TODO 
        IsDead = true;
        AsteroidController.Instance.DespawnAsteroid(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileHandler ph;
        if (collision.TryGetComponent<ProjectileHandler>(out ph))
        {

            _healthRemaining -= ph.Damage;
            //Debug.Log($"bullet impact. health left: {_healthRemaining}");
            ph.ExpireBullet();
            if (_healthRemaining <= 0)
            {
                ExecuteDeath();
            }
        }
    }
}
