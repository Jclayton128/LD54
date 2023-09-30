using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public static ResourceController Instance { get; private set; }

    //settings
    [SerializeField] int _startingMinerals = 100;
    [SerializeField] int _startingScience = 0;

    //state
    int _currentMinerals;
    public int CurrentMinerals => _currentMinerals;
    int _currentScience;
    public int CurrentScience => _currentScience;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameController.Instance.EnterGameMode += InitalizeResources;
    }

    private void InitalizeResources()
    {
        _currentMinerals = _startingMinerals;
        _currentScience = _startingScience;
    }

    public bool CheckMineral(int mineralCost)
    {
        if (mineralCost > _currentMinerals) return false;
        else return true;
    }

    public bool CheckScience(int scienceCost)
    {
        if (scienceCost > _currentScience) return false;
        else return true;
    }
}
