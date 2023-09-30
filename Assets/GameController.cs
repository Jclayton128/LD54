using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Action EnterGameMode;
    public static GameController Instance { get; private set; }

    //state
    float _timeUntilNextAttack = 30f;
    float _timeRemainingOnAttack;
    public float TimeRemainingOnAttack => _timeRemainingOnAttack;
    public float TimeUntilNextAttack => _timeUntilNextAttack;
    float _attackDuration = 15f;
    float _timeBetweenAttacks = 45f;
    bool _isInGame = false;
    public bool IsInGame => _isInGame;
    [SerializeField] bool _isAttackMode = false;
    public bool IsAttackMode => _isAttackMode;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TimeController.Instance.SetProductionTimeRate(0);
    }

    public void StartGameMode()
    {
        _isInGame = true;
        TimeController.Instance.SetProductionTimeRate(1);
        EnterGameMode?.Invoke();
    }

    public void ExitGameMode()
    {
        _isInGame = false;
        TimeController.Instance.SetProductionTimeRate(0);
    }

    private void Update()
    {
        if (!_isInGame) return;
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
        TimeController.Instance.SetProductionTimeRate(0);
    }

    private void EndAttackMode()
    {
        _timeUntilNextAttack = _timeBetweenAttacks;
        _isAttackMode = false;
        UIController.Instance.ExitAttackMode();
        TimeController.Instance.SetProductionTimeRate(1);
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
