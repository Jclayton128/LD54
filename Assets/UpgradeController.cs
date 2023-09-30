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
    List<StructureBrochure> _currentUpgradeOptions;
    int _currentUpgrade = 0;
    StructureLibrary.Structures _currentUpgradeStructureType;
    bool _isUpgrading = false;
    float _currentFactor;
    float _currentTime;
    bool _shouldBeMoving = false;


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
        //check if can upgrade
        _isUpgrading = true;
        _currentFactor = 0;
        _currentTime = 0;

    }

    public void RequestUpgradeCancellation()
    {
        if (!_isUpgrading) return;
         _isUpgrading = false;
        _currentFactor = 0;
        _currentTime = 0;
        _upgradePanelDriver.CancelUpgrade();
    }

    public void LoadUpgradePanel(List<StructureBrochure> upgradeOptions, StructureLibrary.Structures currentType)
    {
        _currentUpgrade = 0;
        _currentUpgradeStructureType = currentType;
        _currentUpgradeOptions = upgradeOptions;
        //Debug.Log($"{upgradeOptions.Count} upgrade options");
        List<Sprite> upgradeIcons = new List<Sprite>();
        foreach (var brochure in upgradeOptions)
        {
            upgradeIcons.Add(brochure.Icon);
        }
        _upgradePanelDriver.LoadUpgradeImages(upgradeIcons);
        _upgradePanelDriver.HighlightUpgrade(_currentUpgrade);
        LoadUpgradeDescriptionPanelWithCurrentUpgrade();
    }

    private void LoadUpgradeDescriptionPanelWithCurrentUpgrade()
    {
        bool isSame = SiteController.Instance.CheckIfSameStructureType(_currentUpgradeStructureType);
        
        
        StructureBrochure brochure = _currentUpgradeOptions[_currentUpgrade];

        bool canAffordMineral = ResourceController.Instance.CheckMineral(brochure.MineralCost);
        bool canAffordScience = ResourceController.Instance.CheckScience(brochure.ScienceCost); ;
        _upgradeDescriptionDriver.LoadDescription(brochure, isSame, canAffordMineral, canAffordScience);
    }

    private void Update()
    {
        if (_isUpgrading)
        {
            _currentTime += Time.deltaTime;
            _currentFactor = _currentTime / TimeLibrary.Instance.ConfirmTime;
            _upgradePanelDriver.SetUpgradeFactor(_currentFactor);
        }
    }
}
