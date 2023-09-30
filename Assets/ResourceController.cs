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
    [SerializeField] int _startingPopulation = 10;
    [SerializeField] int _startingFood = 100;
    [SerializeField] ResourceCountDriver _rsc = null;

    //state
    int _currentMinerals;
    public int CurrentMinerals => _currentMinerals;
    int _currentScience;
    public int CurrentScience => _currentScience;
    int _currentPop;
    public int CurrentPopulation => _currentPop;
    int _currentFood;
    public int CurrentFood => _currentFood;

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
        Debug.Log($"Resetting resources");
        _currentMinerals = _startingMinerals;
        _currentScience = _startingScience;
        _currentFood = _startingFood;
        _currentPop = _startingPopulation;
        _rsc.SetPopulation(_currentPop);
        _rsc.SetFood(_currentFood);
        _rsc.SetMineral(_currentMinerals);
        _rsc.SetScience(_currentScience);
    }

    public void SpendMinerals(int mineralBill)
    {
        if (CheckMineral(mineralBill))
        {
            _currentMinerals -= mineralBill;
            _rsc.SetMineral(_currentMinerals);
        }
    }
    public void SpendFood(int foodBill)
    {
        if (CheckFood(foodBill))
        {
            _currentFood -= foodBill;
            _rsc.SetFood(_currentFood);
        }
    }
    public void SpendScience(int scienceBill)
    {
        if (CheckScience(scienceBill))
        {
            _currentScience -= scienceBill;
            _rsc.SetScience(_currentScience);
        }
    }

    public bool CheckFood(int foodBill)
    {
        if (foodBill > _currentFood) return false;
        else return true;
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
