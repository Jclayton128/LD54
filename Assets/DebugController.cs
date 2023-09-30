using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ttaTMP = null;

    private void Update()
    {
        ListenForQuickAttack();
        UpdateAttackTimer();
    }

    private void UpdateAttackTimer()
    {
        if (GameController.Instance.IsAttackMode)
        {
            _ttaTMP.text = $"Peace in: {GameController.Instance.TimeRemainingOnAttack}";
        }
        else
        {
            _ttaTMP.text = $"Attack in: {GameController.Instance.TimeUntilNextAttack}";
        }

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
    }
}
