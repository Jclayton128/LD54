using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLibrary : MonoBehaviour
{
    public static TimeLibrary Instance { get; private set; }


    //state
    public float EarthRotateTime { get; private set; } = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    public void SetEarthRotateTime(float newTime)
    {
        EarthRotateTime = newTime;
    }
}
