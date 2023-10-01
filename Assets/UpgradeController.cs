using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public static UpgradeController Instance { get; private set; }

    [SerializeField] UpgradePanelDriver _upgradePanelDriver = null;
    [SerializeField] UpgradeDescriptionDriver _upgradeDescriptionDriver = null;

    //state
    List<StructureBrochure> _currentOptionBrochures;
    [SerializeField] int _currentUpgrade = 0;
    StructureLibrary.Structures _currentUpgradeStructureType;
    bool _isUpgrading = false;
    float _currentFactor;
    float _currentTime;
    bool _shouldBeMoving = false;
    bool _canAffordMineral;
    bool _canAffordScience;
    bool _isTechKnown;
    [SerializeField] bool _canBuildHere;
    List<StructureLibrary.Structures> _currentOptionTypes;


    private void Awake()
    {
        Instance = this;
    }

    public void MoveUpgradeLeft()
    {
        if (_shouldBeMoving) return;
        _shouldBeMoving = true;
        Invoke(nameof(CancelShouldBeMoving_Delay), TimeLibrary.Instance.RotateTime);
        _currentUpgrade--;
        if (_currentUpgrade < 0)
        {
            _currentUpgrade = 5;
        }

        _upgradePanelDriver.MoveUpgradeLeft();
        _upgradePanelDriver.HighlightUpgrade(_currentUpgrade);
        LoadUpgradeDescriptionPanelWithCurrentUpgrade();
    }

    public void MoveUpgradeRight()
    {
        if (_shouldBeMoving) return;
        _shouldBeMoving = true;
        Invoke(nameof(CancelShouldBeMoving_Delay), TimeLibrary.Instance.RotateTime);
        _currentUpgrade++;
        if (_currentUpgrade > 5)
        {
            _currentUpgrade = 0;
        }

        _upgradePanelDriver.MoveUpgradeRight();
        _upgradePanelDriver.HighlightUpgrade(_currentUpgrade);
        LoadUpgradeDescriptionPanelWithCurrentUpgrade();
    }

    private void CancelShouldBeMoving_Delay()
    {
        _shouldBeMoving = false;
    }

    public void RequestUpgradeInitiation()
    {
        if (!_canAffordMineral || !_canAffordScience || !_canBuildHere) return;
        //TODO play negative sound
        _isUpgrading = true;
        _currentFactor = 0;
        _currentTime = 0;

    }

    public void RequestUpgradeCancellation()
    {
        //TODO play cancel sound
        if (!_isUpgrading) return;
         _isUpgrading = false;
        _currentFactor = 0;
        _currentTime = 0;
        _upgradePanelDriver.CancelUpgrade();
    }

    private void CompleteUpgrade()
    {
        StructureBrochure brochure = _currentOptionBrochures[_currentUpgrade];
        ResourceController.Instance.SpendMinerals(brochure.MineralCost);
        if (!_isTechKnown)
        {
            ResourceController.Instance.SpendScience(brochure.ScienceCost);
        }
        //TODO play ding/construction sound
        SiteController.Instance.PushNewStructureToSelectedSite(_currentOptionTypes[_currentUpgrade]);
        TechController.Instance.ResearchNewStructure(_currentOptionTypes[_currentUpgrade]);
        _isUpgrading = false;
        _currentFactor = 0;
        _currentTime = 0;
        _upgradePanelDriver.CancelUpgrade();
        SiteController.Instance.HighlightCurrentSite();
        UIController.Instance.ReturnToRotateUponUpgradeCompletion();
    }

    public void LoadUpgradePanel(List<StructureLibrary.Structures> optionTypes, List<StructureBrochure> upgradeBrochures, StructureLibrary.Structures currentType)
    {
        _currentUpgrade = 0;
        _currentUpgradeStructureType = currentType;
        _currentOptionTypes = optionTypes;
        _currentOptionBrochures = upgradeBrochures;
        //Debug.Log($"{upgradeOptions.Count} upgrade options");
        List<Sprite> upgradeIcons = new List<Sprite>();
        foreach (var brochure in upgradeBrochures)
        {
            upgradeIcons.Add(brochure.Icon);
        }
        _upgradePanelDriver.LoadUpgradeImages(upgradeIcons);
        _upgradePanelDriver.HighlightUpgrade(_currentUpgrade);
        LoadUpgradeDescriptionPanelWithCurrentUpgrade();
    }

    private void LoadUpgradeDescriptionPanelWithCurrentUpgrade()
    {
        bool isSame = false;
        if (_currentOptionTypes[_currentUpgrade] == SiteController.Instance.CurrentStructureType) isSame = true;

        StructureBrochure brochure = _currentOptionBrochures[_currentUpgrade];
        _isTechKnown = TechController.Instance.CheckIfStructureIsAlreadyKnown(_currentOptionTypes[_currentUpgrade]);
        _canAffordMineral = ResourceController.Instance.CheckMineral(brochure.MineralCost);
        _canAffordScience = ResourceController.Instance.CheckScience(brochure.ScienceCost); ;
        _canBuildHere = true;
        if (SiteController.Instance.CurrentSite.CurrentStructure.StructureType == StructureLibrary.Structures.Crater)
        {
            _canBuildHere = false;
        }
        if (SiteController.Instance.CurrentSite.CurrentStructure.StructureType == _currentOptionTypes[_currentUpgrade])
        {
            _canBuildHere = false;
        }
        _upgradeDescriptionDriver.LoadDescription(brochure, isSame, _canAffordMineral, _canAffordScience, _isTechKnown);
        _upgradePanelDriver.SetCheckmark(_canAffordMineral, _canAffordScience, _canBuildHere);
    }

    private void Update()
    {
        if (_isUpgrading)
        {
            _currentTime += Time.deltaTime;
            _currentFactor = _currentTime / TimeLibrary.Instance.ConfirmTime;
            _upgradePanelDriver.SetUpgradeFactor(_currentFactor);
            if (_currentFactor >= 1)
            {
                CompleteUpgrade();
            }
        }
    }
}
