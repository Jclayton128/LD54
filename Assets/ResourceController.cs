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
    [SerializeField] float _startingPopulation = 10;
    [SerializeField] int _startingFood = 100;
    [SerializeField] ResourceCountDriver _rsc = null;
    [SerializeField] float _foodConsumeRate = 0.001f; //food per second per person

    //state
    [SerializeField] float _foodDemand;
    int _currentMinerals;
    public int CurrentMinerals => _currentMinerals;
    int _currentScience;
    public int CurrentScience => _currentScience;
    float _currentPop;
    public float CurrentPopulation => _currentPop;
    int _currentFood;
    public int CurrentFood => _currentFood;
    float _timeStep;

    
    //memory
    public bool WasLossByFoodShortage;

    public int CitiesDestroyed;
    public int MineralsMined;
    public int FoodGrown;
    public int YearsSurvived;


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
        //Debug.Log($"Resetting resources");
        _currentMinerals = _startingMinerals;
        _currentScience = _startingScience;
        _currentFood = _startingFood;
        _currentPop = _startingPopulation;
        _foodDemand = 0;
        _rsc.SetPopulation(Mathf.RoundToInt(_currentPop));
        _rsc.SetFood(_currentFood);
        _rsc.SetMineral(_currentMinerals);
        _rsc.SetScience(_currentScience);
        CitiesDestroyed = 0;
        MineralsMined =0;
        FoodGrown =0;
        YearsSurvived = 0;
        _timeStep = 0;
}

    private void Update()
    {
        _timeStep += TimeController.Instance.ProductionDeltaTime;
        if (_timeStep > 2)
        {
            YearsSurvived++;
            _timeStep = 0;
        }

        _foodDemand += TimeController.Instance.ProductionDeltaTime * _foodConsumeRate * _currentPop;
        
        if (_foodDemand > 1)
        {
            //TODO particle, sound of consuming food
            _foodDemand = 0;
            _currentFood -= 1;
            _rsc.SetFood(_currentFood);
            if (_currentFood <= 0)
            {
                WasLossByFoodShortage = true;
                GameController.Instance.CommandGameOver();
            }
        }

        
    
    }

    public void SpendMinerals(int mineralBill)
    {
        if (CheckMineral(mineralBill))
        {
            _currentMinerals -= mineralBill;
            _rsc.SetMineral(_currentMinerals);
        }
        if (mineralBill < 0)
        {
            MineralsMined += Mathf.Abs(mineralBill);
        }
    }
    public void SpendFood(int foodBill)
    {
        if (CheckFood(foodBill))
        {
            _currentFood -= foodBill;
            _rsc.SetFood(_currentFood);
        }
        if (foodBill < 0)
        {
            FoodGrown += Mathf.Abs(foodBill);
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

    public void AddToPopulation(float amountToAdd)
    {
        _currentPop += amountToAdd;
        _rsc.SetPopulation(Mathf.RoundToInt(_currentPop));
        _currentPop = Mathf.Clamp(_currentPop, 0, 9999999);
        if (YearsSurvived > 1 &&  _currentPop < 1)
        {
            WasLossByFoodShortage = false;
            GameController.Instance.CommandGameOver();
        }
        if (amountToAdd < 0)
        {
            CitiesDestroyed++;
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
