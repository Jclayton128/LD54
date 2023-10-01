using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHandler : MonoBehaviour, IActivatable
{
    //settings
    [SerializeField] SpriteRenderer[] _stageBlips = null;
    [SerializeField] int _storageMax = 12;
    [SerializeField] float _timeToMine = 3f;
    [SerializeField] float _timeToHarvest = 1f;
    [SerializeField] bool _isAutoMiner = false;

    //state
    int _currentStorage;
    float _timeRemainingOnCurrentMine;
    float _timeRemainingOnHarvest;
    bool _isHarvesting = false;

    private void Start()
    {
        GameController.Instance.EnteredAttackMode += HandleNewGame;
    }

    private void OnDestroy()
    {
        GameController.Instance.EnteredAttackMode -= HandleNewGame;
    }

    private void HandleNewGame()
    {
        _currentStorage = 0;
        _timeRemainingOnCurrentMine = _timeToMine;
        _timeRemainingOnHarvest = _timeToHarvest;
        _isHarvesting = false;
    }

    public void Activate()
    {
        _isHarvesting =true;
        Debug.Log("Activate");
    }

    public void Deactivate()
    {
        Debug.Log("Deactivate");
        _isHarvesting =false;
        //TODO audio click/chunk when complete;
    }

    private void Update()
    {
        if (_isHarvesting)
        {
            _timeRemainingOnHarvest -= TimeController.Instance.ProductionDeltaTime;
            if (_timeRemainingOnHarvest < 0)
            {
                _timeRemainingOnHarvest = _timeToHarvest;
                _currentStorage--; 
                ResourceController.Instance.SpendMinerals(-1);
                if (_currentStorage <= 0) Deactivate();
            }
        }
        else
        {
            if (_currentStorage >= _storageMax) return;
            _timeRemainingOnCurrentMine -= TimeController.Instance.ProductionDeltaTime;
            if (_timeRemainingOnCurrentMine < 0)
            {
                _timeRemainingOnCurrentMine = _timeToMine;
                if (_isAutoMiner)
                {

                    //TODO particle
                    ResourceController.Instance.SpendMinerals(-1);
                }
                else
                {
                    _currentStorage++;
                }

            }
        }
        ConvertStockToBlips();
    }

    private void ConvertStockToBlips()
    {
        foreach (var blip in _stageBlips)
        {
            blip.color = Color.clear;
        }
        
        for (int i = 0; i < _storageMax; i++)
        {
            if (i < _currentStorage)
            {
                _stageBlips[i].color = ColorLibrary.Instance.HarvestableResource;
            }            
            else
            {
                _stageBlips[i].color = ColorLibrary.Instance.DimResource ;
            }
        }
    }
}
