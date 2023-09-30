using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmHandler : MonoBehaviour, IActivatable
{
    [SerializeField] SpriteRenderer _farmlandSR = null;

    //settings
    [SerializeField] float _timeBetweenStages = 10f;
    [SerializeField] Sprite[] _cropStages = null;
    
    [SerializeField] float _timeToHarvest = 5f;
    [SerializeField] int _foodYield = 0;

    //state
    float _timeInStage;
    int _currentStage;
    float _timeSpentHarvesting;
    [SerializeField] float _factor;
    bool _isHarvesting;
    bool _isHarvestable;

    private void Start()
    {
        GameController.Instance.EnterGameMode += ResetCrop;
    }

    private void ResetCrop()
    {
        _timeInStage = 0;
        _currentStage = 0;
        _timeSpentHarvesting = 0;
        _factor = 0;
        _isHarvesting = false;
        _isHarvestable = false;
    }

    private void Update()
    {
        if (_isHarvesting)
        {
            _timeSpentHarvesting += TimeController.Instance.WallDeltaTime;
            _factor = _timeSpentHarvesting / _timeToHarvest;
            //hook into UI
            if (_factor >= 1)
            {
                ResourceController.Instance.SpendFood(-1 * _foodYield);
                ResetCrop();
                //TODO play a crop sound
            }
        }
        else if (!_isHarvestable)
        {
            _timeInStage += TimeController.Instance.ProductionDeltaTime;
            if (_timeInStage >= _timeBetweenStages)
            {
                _currentStage++;
                _timeBetweenStages = 0;
                Debug.Log("stage incremented");
                if (_currentStage >= _cropStages.Length)
                {
                    _currentStage = _cropStages.Length - 1;
                    _isHarvestable = true;
                    Debug.Log("ready for harvest");
                }
                //_farmlandSR.sprite = _cropStages[_currentStage];
            }
        }
    }

    public void Activate()
    {
        Debug.Log("Activate");
        if (!_isHarvestable) return;
        _isHarvesting = true; 
    }

    public void Deactivate()
    {
        Debug.Log("Deactivate");
        _isHarvesting = false;
    }
}
