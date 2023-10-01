using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] _stageBlips = null;

    [SerializeField] float _maxPopulation = 100;
    [SerializeField] float _initialGrowthRate = 0.001f;

    //state
    float _factor;
    [SerializeField] float _currentPopulation;
    public float Population => _currentPopulation;
    [SerializeField] float _popToAdd;

    private void Start()
    {
        //GameController.Instance.EnterGameMode += HandleNewGame;
        HandleNewGame();
    }

    private void HandleNewGame()
    {
        Invoke(nameof(Delay_HandleNewGame), 0.01f);
        //_currentPopulation = 2;
        //ResourceController.Instance.AddToPopulation(_currentPopulation);
    }

    private void Delay_HandleNewGame()
    {
        _currentPopulation = 2;
        ResourceController.Instance.AddToPopulation(_currentPopulation);
        //Debug.Log($"adding {_currentPopulation} pop. now at {ResourceController.Instance.CurrentPopulation}");
    }

    void Update()
    {
        _popToAdd = TimeController.Instance.ProductionDeltaTime * _initialGrowthRate * TechController.Instance.BonusGrowthRate *
            ((_maxPopulation - _currentPopulation) / _maxPopulation) * (_currentPopulation);
        _currentPopulation += _popToAdd;
        ResourceController.Instance.AddToPopulation(_popToAdd);
        _factor = _currentPopulation / _maxPopulation;
        ConvertStockToBlips();
    }

    private void OnDestroy()
    {
        ResourceController.Instance.AddToPopulation(-1*_currentPopulation);
        //GameController.Instance.EnterGameMode -= HandleNewGame;
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
