using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    Rigidbody2D _rb;

    SpriteRenderer _sr;
    [SerializeField] Sprite[] _missileSizes = null;
    Collider2D _coll;

    //state
    bool _isExpiring;
    public int Damage;
    float _timeToExpire;


    public void Activate(int damage, float lifetime, float speed)
    {
        _coll.enabled = true;
        Damage = damage + TechController.Instance.BonusDamage;
        _timeToExpire = Time.time + lifetime;
        _rb.velocity = transform.up * speed;

        _sr.enabled = true;
        if (damage == 1) _sr.sprite = _missileSizes[0];
        else if (damage > 6) _sr.sprite = _missileSizes[2];
        else _sr.sprite = _missileSizes[1];
        _isExpiring = false;
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
        if (_isExpiring) return;
        _sr.enabled = false;
        _isExpiring = true;
        _coll.enabled = false;
        _rb.velocity = Vector3.zero;
        Invoke(nameof(Delay_ExpireBullet), 5f);
    }

    private void Delay_ExpireBullet()
    {
        ProjectileController.Instance.DespawnProjectile(this);
    }

    internal void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _coll = GetComponent<Collider2D>();
    }
}
