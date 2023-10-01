using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    Rigidbody2D _rb;

    //state
    public int Damage;
    float _timeToExpire;


    public void Activate(int damage, float lifetime, float speed)
    {
        Damage = damage;
        _timeToExpire = Time.time + lifetime;
        _rb.velocity = transform.up * speed;
    }


    private void Update()
    {
        if (Time.time > _timeToExpire)
        {
            ExpireBullet();
        }
    }
    public void ExpireBullet()
    {
        ProjectileController.Instance.DespawnProjectile(this);
    }

    internal void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }
}
