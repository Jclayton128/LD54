using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance { get; private set; }

    //state
    [SerializeField] float _productionTimescale = 1;
    public float ProductionDeltaTime => _productionTimescale * Time.deltaTime;

    public float WallDeltaTime => Time.deltaTime;

    private void Awake()
    {
        Instance = this;
    }

    public void SetProductionTimeRate(float factor)
    {
        _productionTimescale = factor;
    }
}
