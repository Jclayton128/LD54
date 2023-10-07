using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Action EnterGameMode;
    public Action EnteredAttackMode;
    public Action ExitedAttackMode;
    public static GameController Instance { get; private set; }

    //state
    int _attackWavesEndured;
    public int Difficulty => 1 + _attackWavesEndured;
    [SerializeField] float _timeUntilNextAttack;
    //float _timeRemainingOnAttack;
    //public float TimeRemainingOnAttack => _timeRemainingOnAttack;
    public float TimeUntilNextAttack => _timeUntilNextAttack;



    //float _attackDuration = 15f;
    float _timeBetweenAttacks_starting = 40f;
    bool _isInGame = false;
    public bool IsInGame => _isInGame;
    [SerializeField] bool _isAttackMode = false;
    public bool IsAttackMode => _isAttackMode;
    float _currentTimeBetweenAttacks;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TimeController.Instance.SetProductionTimeRate(0);
        AsteroidController.Instance.LastAsteroidKilled += EndAttackMode;
    }

    public void StartGameMode()
    {
        _attackWavesEndured = 0;
           _isInGame = true;
        _currentTimeBetweenAttacks = _timeBetweenAttacks_starting;
        _timeUntilNextAttack = _currentTimeBetweenAttacks;
        TimeController.Instance.SetProductionTimeRate(1);
        EnterGameMode?.Invoke();
    }

    public void ExitGameMode()
    {
        _isInGame = false;
        TimeController.Instance.SetProductionTimeRate(0);
        
    }

    public void CommandGameOver()
    {
        AsteroidController.Instance.StopSpawningAsteroids();
        UIController.Instance.EnterEndGame();
    }


    private void Update()
    {
        if (!_isInGame) return;
        if (!_isAttackMode)
        {
            _timeUntilNextAttack -= Time.deltaTime;

            if (!TutorialController.instance.EndedTutorial)
            {
                _timeUntilNextAttack = 15;
            }

            if (_timeUntilNextAttack < 0)
            {
                EnterAttackMode();
            }
        }
        else
        {
            //_timeRemainingOnAttack -= Time.deltaTime;
            //if (_timeRemainingOnAttack < 0)
            //{
            //    _timeUntilNextAttack = _timeBetweenAttacks;
            //    _isAttackMode = false;
            //    ExitedAttackMode?.Invoke();
            //}
        }

    }

    private void EnterAttackMode()
    {
        //_timeRemainingOnAttack = _attackDuration;
        _isAttackMode = true;
        UIController.Instance.EnterAttackMode();
        TimeController.Instance.SetProductionTimeRate(0);
        EnteredAttackMode?.Invoke();
    }

    internal void ForceImminentAttack()
    {
        _timeUntilNextAttack = 13f;
    }

    private void EndAttackMode()
    {
        _attackWavesEndured++;
        _isAttackMode = false;
        _currentTimeBetweenAttacks -= 1;
        _currentTimeBetweenAttacks = Mathf.Clamp(_currentTimeBetweenAttacks, 10, 99);
        _timeUntilNextAttack = _currentTimeBetweenAttacks;
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
        Debug.Log($"difficulty now: {Difficulty}");
    }

    internal void Debug_DelayAttack()
    {
        _timeUntilNextAttack = 120f;
        Debug.Log("Delaying Attack. TTA: " + _timeUntilNextAttack);
    }


}
