using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] _stageBlips = null;
    [SerializeField] SpriteRenderer _sr = null;

    float _maxPopulation = 25000;
    [SerializeField] float _initialGrowthRate = 0.001f;
    [SerializeField] Sprite[] _citySprites = null;

    //state
    float _factor;
    float _growthRate;
    [SerializeField] float _currentPopulation;
    public float Population => _currentPopulation;
    [SerializeField] float _popToAdd;
    ParticleSystem _ps;
    float _blip;

    private void Start()
    {
        //GameController.Instance.EnterGameMode += HandleNewGame;
        HandleNewGame();
        _ps = GetComponentInChildren<ParticleSystem>();
        _growthRate = UnityEngine.Random.Range(0.5f, 1.5f) * _initialGrowthRate;
    }

    public void AddPopulation(float amount)
    {
        _currentPopulation += amount;
    }

    private void HandleNewGame()
    {
        Invoke(nameof(Delay_HandleNewGame), 0.01f);
        //_currentPopulation = 2;
        //ResourceController.Instance.AddToPopulation(_currentPopulation);
    }

    private void Delay_HandleNewGame()
    {
        _currentPopulation = 1;
        ResourceController.Instance.RegisterHouse(this);
        //ResourceController.Instance.AddToPopulation(_currentPopulation);
        //Debug.Log($"adding {_currentPopulation} pop. now at {ResourceController.Instance.CurrentPopulation}");
    }

    void Update()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        if (rand == 1)
        {
            if (ResourceController.Instance.CurrentFood <= 0)
            {
                _popToAdd = -.05f * _currentPopulation * TimeController.Instance.ProductionDeltaTime;
                _currentPopulation += _popToAdd;
            }
            else
            {
                _popToAdd = TimeController.Instance.ProductionDeltaTime * (_growthRate + TechController.Instance.BonusGrowthRate) *
    ((ResourceController.Instance.CurrentFood * 100 - _currentPopulation) / _maxPopulation) * (_currentPopulation);
                _currentPopulation += _popToAdd / 2f;
            }

        }
        
        //_blip += _popToAdd;
        //if (_blip > 1)
        //{
        //    _blip = 0;
        //    _ps.Emit(1);
        //}

        //ResourceController.Instance.AddToPopulation(_popToAdd);
        _factor = _currentPopulation / _maxPopulation;
        //ConvertStockToBlips();
        ConvertStockToVisuals();
    }

    private void ConvertStockToVisuals()
    {
        if (_currentPopulation > 12500)
        {
            _sr.sprite = _citySprites[3];
        }
        else if (_currentPopulation > 2500)
        {
            _sr.sprite = _citySprites[2];
        }
        else if (_currentPopulation > 50)
        {
            _sr.sprite = _citySprites[1];
        }
        else if (_currentPopulation > 0)
        {
            _sr.sprite = _citySprites[0];
        }
        else
        {
            _sr.sprite = null;
        }
    }

    private void OnDestroy()
    {

        //GameController.Instance.EnterGameMode -= HandleNewGame;
    }

    public void HandleImpact(int damage)
    {
        float rand = UnityEngine.Random.Range(0.8f, 1.2f);
        float dead = (float)damage * 10 * rand;
        _currentPopulation -= dead;
        //ResourceController.Instance.AddToPopulation(-1 * dead);
        Debug.Log($"Blammo! Just killed {dead}. {_currentPopulation} remain");
        _currentPopulation = Mathf.Clamp(_currentPopulation, 0, 999999);
        ConvertStockToVisuals();
    }

    private void ConvertStockToBlips()
    {
        if (_factor < 0.2f)
        {
            _stageBlips[0].color = ColorLibrary.Instance.DimResource;
            _stageBlips[1].color = ColorLibrary.Instance.DimResource;
            _stageBlips[2].color = ColorLibrary.Instance.DimResource;
        }
        else if (_factor < 0.5f)
        {
            _stageBlips[0].color = ColorLibrary.Instance.BrightResource;
            _stageBlips[1].color = ColorLibrary.Instance.DimResource;
            _stageBlips[2].color = ColorLibrary.Instance.DimResource;
        }
        else if (_factor < 0.75f)
        {
            _stageBlips[0].color = ColorLibrary.Instance.BrightResource;
            _stageBlips[1].color = ColorLibrary.Instance.BrightResource;
            _stageBlips[2].color = ColorLibrary.Instance.DimResource;
        }
        else if (_factor < .9f)
        {
            _stageBlips[0].color = ColorLibrary.Instance.BrightResource;
            _stageBlips[1].color = ColorLibrary.Instance.BrightResource;
            _stageBlips[2].color = ColorLibrary.Instance.BrightResource;
        }
        else
        {
            _stageBlips[0].color = ColorLibrary.Instance.BrightResource;
            _stageBlips[1].color = ColorLibrary.Instance.BrightResource;
            _stageBlips[2].color = ColorLibrary.Instance.BrightResource;
        }


    }

}
