using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugController : MonoBehaviour
{
    private void Update()
    {
        ListenForQuickAttack();
    }   

    private void ListenForQuickAttack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameController.Instance.Debug_ForceImminentAttack();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameController.Instance.Debug_ForceEndAttack();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameController.Instance.Debug_DelayAttack();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (GameController.Instance.IsAttackMode)
            {
                AsteroidController.Instance.SpawnNewAsteroid();
            }
        }
    }
}
