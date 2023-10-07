using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHandler : MonoBehaviour
{
    SpriteRenderer _sr;
    Rigidbody2D _rb;

    [SerializeField] Sprite[] _asteroidBigToSmall = null;

    //state
    int _healthRemaining;
    public int Damage => _healthRemaining;
     public bool IsDead;
    public Vector3 Velocity => _rb.velocity;

    public void Initialize()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Activate(float speed, int health)
    {
        _healthRemaining = health;
        ConvertHealthToSprite();
        Vector3 dir = (Vector3.zero - transform.position).normalized;
        _rb.velocity = dir * speed;
        IsDead = false;
    }

    private void ConvertHealthToSprite()
    {
        if (_healthRemaining > 5)
        {
            _sr.sprite = _asteroidBigToSmall[0];
        }
        else if (_healthRemaining > 3)
        {
            _sr.sprite = _asteroidBigToSmall[1];
        }
        else if (_healthRemaining > 1)
        {
            _sr.sprite = _asteroidBigToSmall[2];
        }
        else
        {
            _sr.sprite = _asteroidBigToSmall[3];
        }
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
            ConvertHealthToSprite();
            //Debug.Log($"bullet impact. health left: {_healthRemaining}");
            ph.ExpireBullet();
            if (_healthRemaining <= 0)
            {
                ExecuteDeath();
            }
        }
    }
}
