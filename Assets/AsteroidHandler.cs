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

    public void Initialize()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Activate(float speed, int health)
    {
        _healthRemaining = health;
        
        Vector3 dir = (Vector3.zero - transform.position);
        _rb.velocity = dir * speed;
    }

    public void HandleStructureImpact()
    {
        _healthRemaining = 0;
        ExecuteDeath();
    }

    private void ExecuteDeath()
    {
        //TODO 
        AsteroidController.Instance.DespawnAsteroid(this);
    }

    public void HandleBulletImpact()
    {
        _healthRemaining--;
        if (_healthRemaining <= 0)
        {
            ExecuteDeath();
        }
    }
}
