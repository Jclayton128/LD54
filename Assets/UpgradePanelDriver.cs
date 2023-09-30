using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradePanelDriver : MonoBehaviour
{
    [SerializeField] Image[] _upgradeOptionImages = null;
    [SerializeField] Image[] _upgradeOptionBackgrounds = null;

    [SerializeField] RotationHandler _upgradeRotationhandler = null;
    [SerializeField] Image _growthImage = null;
    [SerializeField] Image _checkImage = null;

    //state
    Tween _upgradeBar;

    private void Start()
    {
        CancelUpgrade();
    }

    public void SetUpgradeFactor(float factor)
    {
        _upgradeBar.Kill();
        _growthImage.fillAmount = factor;
    }

    public void CancelUpgrade()
    {
        _upgradeBar.Kill();
        _upgradeBar = _growthImage.DOFillAmount(0, 0.3f);
    }

    public void LoadUpgradeImages(List<Sprite> upgradeIcons)
    {
        if (upgradeIcons.Count > _upgradeOptionImages.Length)
        {
            //Debug.LogWarning("more upgrade icons that can be displayed!");
        }
        foreach (var image in _upgradeOptionImages)
        {
            image.sprite = null;
            //Debug.Log("nullin image");
        }
        for (int i = 0; i < upgradeIcons.Count; i++)
        {
            _upgradeOptionImages[i].sprite = upgradeIcons[i];
            //Debug.Log("adding valuable image");
        }
    }

    public void MoveUpgradeLeft()
    {
        _upgradeRotationhandler.CommandRotation(-1);
        foreach (var image in _upgradeOptionImages)
        {
            image.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void MoveUpgradeRight()
    {
        _upgradeRotationhandler.CommandRotation(1);
        foreach (var image in _upgradeOptionImages)
        {
            image.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void HighlightUpgrade(int index)
    {
        foreach (var image in _upgradeOptionBackgrounds)
        {
            image.color = ColorLibrary.Instance.LowlightedUpgrade;
        }
        _upgradeOptionBackgrounds[index].color = ColorLibrary.Instance.HighlightedUpgrade;
    }

    private void Update()
    {
        foreach (var image in _upgradeOptionImages)
        {
            image.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        _growthImage.transform.rotation = Quaternion.Euler(0, 0, 0);
        _checkImage.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
