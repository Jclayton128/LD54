using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceCountDriver : MonoBehaviour
{
    //scene refs

    [SerializeField] TextMeshProUGUI _population = null;
    [SerializeField] TextMeshProUGUI _food = null;
    [SerializeField] TextMeshProUGUI _mineral = null;
    [SerializeField] TextMeshProUGUI _science = null;

    public void SetPopulation(int newCount)
    {
        _population.text = newCount.ToString();
    }
    public void SetFood(int newCount)
    {
        _food.text = newCount.ToString();
    }
    public void SetMineral(int newCount)
    {
        _mineral.text = newCount.ToString();
    }

    public void SetScience(int newCount)
    {
        _science.text = newCount.ToString();
    }
}
