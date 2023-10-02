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
            _ttaTMP.text = $"DANGER!";
            _ttaTMP.color = Color.red;
        }
        else
        {
            if ((GameController.Instance.TimeUntilNextAttack) > 14)
            {
                _ttaTMP.text = " ";
            }
            else if ((GameController.Instance.TimeUntilNextAttack) < 5)
            {
                _ttaTMP.text = $"Imminent Rock Strike!";
                _ttaTMP.color = Color.yellow;
            }
            else 
            {
                _ttaTMP.text = $"Inbound Rocks Detected!";
                _ttaTMP.color = Color.white;
            }

            
        }

    }   
}
