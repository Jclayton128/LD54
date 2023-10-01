using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndPanelDriver : MonoBehaviour
{
    [SerializeField] TextMeshPro _gameoverTMP_main = null;
    [SerializeField] TextMeshPro _gameoverTMP_sub = null;
    [SerializeField] TextMeshPro _finalPop = null;
    [SerializeField] TextMeshPro _rocksKilled = null;
    [SerializeField] TextMeshPro _yearsSurvived = null;
    [SerializeField] TextMeshPro _cropsGrown = null;
    [SerializeField] TextMeshPro _mineralsMined = null;
    [SerializeField] TextMeshPro _citiesDestroyed = null;

    public void PopulateSelf()
    {
        _citiesDestroyed.text = "Cities Destroyed: \n" + ResourceController.Instance.CitiesDestroyed;
        _mineralsMined.text = "Minerals Mined: \n" + ResourceController.Instance.MineralsMined;
        _cropsGrown.text = "Food Grown: \n" + ResourceController.Instance.FoodGrown;
        _yearsSurvived.text = "Years Survived: \n" + ResourceController.Instance.YearsSurvived;
        _rocksKilled.text = "Rock Blasted: \n" + AsteroidController.Instance.RocksKilled;
        _finalPop.text = "Final Population: \n" + Mathf.RoundToInt(ResourceController.Instance.CurrentPopulation).ToString();

        if (ResourceController.Instance.WasLossByFoodShortage)
        {
            _gameoverTMP_main.text = "Food Supply Depleted!";
            _gameoverTMP_sub.text = "Malthus wins again...";
        }
        else
        {
            _gameoverTMP_main.text = "No Living Humans!";
            _gameoverTMP_sub.text = "We had a good run...";
        }
    }

}
