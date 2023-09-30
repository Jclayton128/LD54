using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteController : MonoBehaviour
{

    public static SiteController Instance { get; private set; }

    [SerializeField] SiteHandler[] _sites = null;

    //state
    int _currentSite = 0;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HighlightCurrentSite();
        UIController.Instance.RotationRequired += HandleSiteSelectionChanged;
    }

    private void HandleSiteSelectionChanged(int dir)
    {
        _currentSite += dir;
        if (_currentSite < 0) _currentSite = _sites.Length - 1;
        else if (_currentSite >= _sites.Length) _currentSite = 0;
        HighlightCurrentSite();
    }

    private void HighlightCurrentSite()
    {
        foreach (var site in _sites)
        {
            site.Lowlight();
        }
        _sites[_currentSite].Highlight();
    }


}
