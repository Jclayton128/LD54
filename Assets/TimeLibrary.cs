using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLibrary : MonoBehaviour
{
    public static float EarthRotateTime { get; private set; } = 1f;

    public void SetEarthRotateTime(float newTime)
    {
        EarthRotateTime = newTime;
    }
}
