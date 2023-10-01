using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ttaTMP = null;

    private void Update()
    {
        UpdateAttackTimer();
    }

    private void UpdateAttackTimer()
    {
        if (GameController.Instance.IsAttackMode)
        {
            _ttaTMP.text = $"Imminent Rock Strikes!";
        }
        else
        {
            _ttaTMP.text = $"Rocks Inbound: {Mathf.RoundToInt(GameController.Instance.TimeUntilNextAttack)}";
        }

    }   
}
