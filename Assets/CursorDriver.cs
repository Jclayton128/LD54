using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorDriver : MonoBehaviour
{
    [SerializeField] Image _cursorImage = null;

    public void HideCursor()
    {
        _cursorImage.color = Color.clear;
    }

    public void ShowCursor()
    {
        _cursorImage.color = Color.white;
    }

}
