using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmHandler : MonoBehaviour, IActivatable
{

    //settings
    [SerializeField] SpriteRenderer[] _stageBlips = null;
    [SerializeField] float _timeBetweenStages = 5f;
    //[SerializeField] int _cropStages = 5;
    
    [SerializeField] float _timeToHarvest = 20f;
    [SerializeField] int _foodYield = 0;
    [SerializeField] CuePulser _harvestCue = null;

    //state
    [SerializeField] float _timeInStage;
    [SerializeField] int _currentStage;
    float _timeSpentHarvesting;
    [SerializeField] float _factor;
    bool _isHarvesting;
    bool _isHarvestable;
    ParticleSystem _ps;
    ParticleSystem.MainModule _psem;
    float _blipTime;

    private void Awake()
    {
        _ps = GetComponentInChildren<ParticleSystem>();
        _psem = _ps.main;
    }

    private void Start()
    {
        GameController.Instance.EnterGameMode += ResetCrop;
    }

    private void ResetCrop()
    {
        _harvestCue.gameObject.SetActive(false);
        _timeInStage = 0;
        _currentStage = 0;
        _timeSpentHarvesting = 0;
        _factor = 0;
        _isHarvesting = false;
        _isHarvestable = false;
        ConvertStageIntoBlips();
    }

    private void Update()
    {
        if (_isHarvesting)
        {
            _timeSpentHarvesting += TimeController.Instance.WallDeltaTime;
            _factor = _timeSpentHarvesting / _timeToHarvest;
            _blipTime += TimeController.Instance.WallDeltaTime;

            if (_blipTime > 0.25)
            {
                _ps.Emit(1);
                //ResourceController.Instance.SpendFood(-1 * _foodYield);
                _blipTime = 0;
            }

            //hook into UI
            if (_factor >= 1)
            {
                ResourceController.Instance.SpendFood(-1 * _foodYield*10);
                _ps.Emit(_foodYield *2);
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
                _timeInStage = 0;
                //Debug.Log("stage incremented");
                if (_currentStage >= _stageBlips.Length)
                {
                    _currentStage = _stageBlips.Length;
                    _isHarvestable = true;
                    _harvestCue.gameObject.SetActive(true);
                    //Debug.Log("ready for harvest");
                   
                }
                //ConvertStageIntoBlips();
                //_farmlandSR.sprite = _cropStages[_currentStage];
            }
            ConvertStageIntoBlips();
        }
    }

    public void ConvertStageIntoBlips()
    {
        switch (_currentStage)
        {
            case 0:
                _stageBlips[0].color = ColorLibrary.Instance.DimResource;
                _stageBlips[1].color = ColorLibrary.Instance.DimResource;
                _stageBlips[2].color = ColorLibrary.Instance.DimResource;
                _stageBlips[3].color = ColorLibrary.Instance.DimResource;
                break;

            case 1:
                _stageBlips[0].color = ColorLibrary.Instance.BrightResource;
                _stageBlips[1].color = ColorLibrary.Instance.DimResource;
                _stageBlips[2].color = ColorLibrary.Instance.DimResource;
                _stageBlips[3].color = ColorLibrary.Instance.DimResource;
                break;

            case 2:
                _stageBlips[0].color = ColorLibrary.Instance.BrightResource;
                _stageBlips[1].color = ColorLibrary.Instance.BrightResource;
                _stageBlips[2].color = ColorLibrary.Instance.DimResource;
                _stageBlips[3].color = ColorLibrary.Instance.DimResource;
                break;

            case 3:
                _stageBlips[0].color = ColorLibrary.Instance.BrightResource;
                _stageBlips[1].color = ColorLibrary.Instance.BrightResource;
                _stageBlips[2].color = ColorLibrary.Instance.BrightResource;
                _stageBlips[3].color = ColorLibrary.Instance.DimResource;
                break;

            case 4:
                _stageBlips[0].color = ColorLibrary.Instance.HarvestableResource;
                _stageBlips[1].color = ColorLibrary.Instance.HarvestableResource;
                _stageBlips[2].color = ColorLibrary.Instance.HarvestableResource;
                _stageBlips[3].color = ColorLibrary.Instance.HarvestableResource;
                break;




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
        _blipTime = 0;
    }
}
