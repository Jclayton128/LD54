using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAimHandler : MonoBehaviour
{
    //settings
    [SerializeField] float _timeBetweenScans = 0.3f;
    [SerializeField] float _scanRange = 5;
    [SerializeField] float _scanMidpoint = 2.5f;
    [SerializeField] float _rotateRate = 30f;
    [SerializeField] Transform _barrel = null;


    //state
    int _layerMask = 1 << 7;
    public AsteroidHandler Target;
    Vector3 _trueDir;
    Vector3 _currentDir;
    float _timeForNextScan;


    private void Start()
    {
        GameController.Instance.EnteredAttackMode += EnterAttackMode;
        GameController.Instance.ExitedAttackMode += ExitAttackMode;
    }

    private void EnterAttackMode()
    {
        //_isAiming = true;
    }

    private void ExitAttackMode()
    {
        //_isAiming = false;
    }

    private void Delay_ExitAttackMode()
    {

    }

    private void Update()
    {
        if (Target && Target.IsDead) Target = null;

        if (Time.time >= _timeForNextScan)
        {
            ScanForTarget();
            _timeForNextScan = Time.time + _timeBetweenScans;
        }

        AimAtTarget();
    }

    private void ScanForTarget()
    {
        var hit = Physics2D.OverlapCircle(transform.position + (transform.up * _scanMidpoint), _scanRange, _layerMask);
        if (hit != null) Target = hit.GetComponent<AsteroidHandler>();
        //Debug.DrawLine(transform.position + (transform.up * _scanMidpoint), transform.position, Color.blue, _timeBetweenScans);
    }

    private void AimAtTarget()
    {
        if (Target != null)
        {
            _trueDir = (Target.transform.position + (Target.Velocity/4f) - transform.position).normalized;
        }
        else
        {
            _trueDir = transform.up;
        }

        _currentDir = Vector2.Lerp(_barrel.up, _trueDir, _rotateRate * Time.deltaTime);
        _barrel.up = _currentDir;
    }

    private void OnDestroy()
    {
        GameController.Instance.EnteredAttackMode -= EnterAttackMode;
        GameController.Instance.ExitedAttackMode -= ExitAttackMode;
    }
}
