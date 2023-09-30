using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDriver : MonoBehaviour
{
    public void ShowHidePanel(bool shouldShow)
    {
        if (shouldShow) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
