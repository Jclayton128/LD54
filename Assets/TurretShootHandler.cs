using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootHandler : MonoBehaviour
{
    [SerializeField] float _timeBetweenShots = 1f;

    [SerializeField] float _shotSpeed = 3f;
    [SerializeField] float _shotTime = 10f;
    [SerializeField] int _damage = 1;
    [SerializeField] Transform _muzzle;

    //state
    float _timeForNextShot;
    TurretAimHandler _aim;

    private void Awake()
    {
        _aim = GetComponent<TurretAimHandler>();
    }

    private void Start()
    {
        _timeForNextShot = 0;
    }

    private void Update()
    {
        if (_aim.Target != null && Time.time > _timeForNextShot)
        {
            Shoot();
            _timeForNextShot = Time.time + _timeBetweenShots;
        }
    }

    private void Shoot()
    {
        ProjectileController.Instance.SpawnProjectile(_muzzle, _damage, _shotSpeed, _shotTime);
    }
}
