using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteController : MonoBehaviour
{
    public enum GeneralSites { Empty, Cratered, House, Farm, Mine, Lab, Military}
    public static SiteController Instance { get; private set; }

    [SerializeField] SiteHandler[] _sites = null;

    //state
    [SerializeField] int _currentSite = 0;
    public SiteHandler CurrentSite => _sites[_currentSite];
    StructureLibrary.Structures _currentStructureType;
    public StructureLibrary.Structures CurrentStructureType => _currentStructureType;

    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameController.Instance.EnterGameMode += HighlightCurrentSite;
        UIController.Instance.SiteSelectionChanged += HandleSiteSelectionChanged;
    }



    private void HandleSiteSelectionChanged(int dir)
    {
        _currentSite += dir;
        if (_currentSite < 0) _currentSite = _sites.Length - 1;
        else if (_currentSite >= _sites.Length) _currentSite = 0;
        _currentStructureType = _sites[_currentSite].CurrentStructure.StructureType;
        HighlightCurrentSite();
    }

    private void LoadUpgradePanelForCurrentSite()
    {
        List<StructureLibrary.Structures> upgradeOptions = _sites[_currentSite].CurrentStructure.UpgradeOptions;
        List<StructureBrochure> brochures = new List<StructureBrochure>();
        for (int i = 0; i < upgradeOptions.Count; i++)        {

            brochures.Add(StructureLibrary.Instance.GetBrochureFromMenu(upgradeOptions[i]));
            //Debug.Log($"including upgrade to {StructureLibrary.Instance.GetBrochureFromMenu(upgradeOptions[i]).SName}");
        }
        UpgradeController.Instance.LoadUpgradePanel(upgradeOptions, brochures, _currentStructureType);
    }

    private void HighlightCurrentSite()
    {
        foreach (var site in _sites)
        {
            site.Lowlight();
        }
        _sites[_currentSite].Highlight();
        LoadUpgradePanelForCurrentSite();
    }

    public void PushNewStructureToSelectedSite(StructureLibrary.Structures newStructure)
    {
        _sites[_currentSite].ReceiveNewStructure(newStructure);
    }
}
