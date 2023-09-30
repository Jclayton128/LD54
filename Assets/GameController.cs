using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    //state
    float _timeUntilNextAttack = 30f;
    float _timeRemainingOnAttack;
    public float TimeRemainingOnAttack => _timeRemainingOnAttack;
    public float TimeUntilNextAttack => _timeUntilNextAttack;
    float _attackDuration = 15f;
    float _timeBetweenAttacks = 45f;
    [SerializeField] bool _isAttackMode = false;
    public bool IsAttackMode => _isAttackMode;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (!_isAttackMode)
        {
            _timeUntilNextAttack -= Time.deltaTime;
            if (_timeUntilNextAttack < 0)
            {
                EnterAttackMode();
            }
        }
        else
        {
            _timeRemainingOnAttack -= Time.deltaTime;
            if (_timeRemainingOnAttack < 0)
            {
                EndAttackMode();
            }
        }

    }

    private void EnterAttackMode()
    {
        _timeRemainingOnAttack = _attackDuration;
        _isAttackMode = true;
        UIController.Instance.EnterAttackMode();
    }

    private void EndAttackMode()
    {
        _timeUntilNextAttack = _timeBetweenAttacks;
        _isAttackMode = false;
        UIController.Instance.ExitAttackMode();
    }

    public void Debug_ForceImminentAttack()
    {
        _timeUntilNextAttack = 1f;
    }

    public void Debug_ForceEndAttack()
    {
        EndAttackMode();
    }
}
