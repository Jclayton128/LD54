using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLibrary : MonoBehaviour
{
    public static TimeLibrary Instance { get; private set; }


    //state
    public float RotateTime { get; private set; } = 0.5f;
    public float ConfirmTime { get; private set; } = 1f;

    public float CameraMoveTime { get; private set; } = 3f;
    public float CameraZoomTime { get; private set; } = 1.5f;

    private void Awake()
    {
        Instance = this;
    }

    public void SetEarthRotateTime(float newTime)
    {
        RotateTime = newTime;
    }
}
