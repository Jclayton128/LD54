using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    public Action<int> RotationCommanded;
    public Action UpDepressed;
    public Action UpReleased;
    public Action DownDepressed;
    public Action DownReleased;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotationCommanded?.Invoke(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            RotationCommanded?.Invoke(1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpDepressed?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DownDepressed?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            DownReleased?.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            UpReleased?.Invoke();
        }

        //if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    RotationCeased?.Invoke();
        //}
    }


}
