using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    Rigidbody2D _rb;

    SpriteRenderer _sr;
    [SerializeField] Sprite[] _missileSizes = null;

    //state
    public int Damage;
    float _timeToExpire;


    public void Activate(int damage, float lifetime, float speed)
    {
        Damage = damage + TechController.Instance.BonusDamage;
        _timeToExpire = Time.time + lifetime;
        _rb.velocity = transform.up * speed;

        if (damage == 1) _sr.sprite = _missileSizes[0];
        else if (damage > 6) _sr.sprite = _missileSizes[2];
        else _sr.sprite = _missileSizes[1];

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
        _sr = GetComponent<SpriteRenderer>();
    }
}
