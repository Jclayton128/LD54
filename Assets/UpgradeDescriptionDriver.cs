using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeDescriptionDriver : MonoBehaviour
{
    //scene refs
    [SerializeField] TextMeshProUGUI _name = null;
    [SerializeField] TextMeshProUGUI _description = null;
    [SerializeField] Image _icon = null;
    [SerializeField] TextMeshProUGUI _mineralCost = null;
    [SerializeField] TextMeshProUGUI _scienceCost = null;

    public void LoadDescription(StructureBrochure brochure, bool isSameThing, bool canAffordMineral, bool canAffordScience)
    {
        _name.text = brochure.SName;
        _description.text = brochure.Description;
        _icon.sprite = brochure.Icon;
        //Debug.Log($"min cost: {brochure.MineralCost}");

        if (isSameThing)
        {
            _mineralCost.text = "-";
            _mineralCost.color = ColorLibrary.Instance.AffordableUpgrade;
            _scienceCost.text = "-";
            _scienceCost.color = ColorLibrary.Instance.AffordableUpgrade;
        }
        else
        {
            _mineralCost.text = brochure.MineralCost.ToString();
            if (canAffordMineral)
            {
                _mineralCost.color = ColorLibrary.Instance.AffordableUpgrade;
            }
            else
            {
                _mineralCost.color = ColorLibrary.Instance.UnaffordableUpgrade;
            }
            _scienceCost.text = brochure.ScienceCost.ToString();
            if (canAffordScience)
            {
                _scienceCost.color = ColorLibrary.Instance.AffordableUpgrade;
            }
            else
            {
                _scienceCost.color = ColorLibrary.Instance.UnaffordableUpgrade;
            }
        }
        
    }
}
