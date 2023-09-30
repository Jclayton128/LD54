using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    public Action<int> RotationCommanded;
    public Action RotationCeased;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotationCommanded?.Invoke(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotationCommanded?.Invoke(1);
        }
        
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            RotationCeased?.Invoke();
        }
    }


}
