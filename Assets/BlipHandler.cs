using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIController.Instance.ModeChanged += HandleModeChanged;     
    }

    private void HandleModeChanged(UIController.Mode newMode)
    {
        if (newMode != UIController.Mode.Rotate) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        UIController.Instance.ModeChanged -= HandleModeChanged;
    }
}
