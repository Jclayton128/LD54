using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Action<int> RotationRequired;
    public static UIController Instance { get; private set; }

    //scene references
    [SerializeField] CursorDriver[] _cursors = null;

    //state
    bool _shouldEarthBeRotating = false;
    int _cursorIndex;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _cursorIndex = 1;
        ShowCurrentCursor();
        InputController.Instance.RotationCommanded += HandleRotationCommanded;
    }

    private void HandleRotationCommanded(int dir)
    {
        if (dir < 0) MoveCursorLeft();
        if (dir > 0) MoveCursorRight();
    }

    private void ShowCurrentCursor()
    {
        foreach (var cursor in _cursors)
        {
            cursor.HideCursor();
        }
        _cursors[_cursorIndex].ShowCursor();
    }

    public void MoveCursorLeft()
    {
        if (_shouldEarthBeRotating) return;
        _cursorIndex--;
        if (_cursorIndex < 0)
        {
            _cursorIndex = _cursors.Length-1;
            RotationRequired?.Invoke(-1);
            _shouldEarthBeRotating = true;
            Invoke(nameof(CancelEarthRotation_Delay), TimeLibrary.EarthRotateTime);
        }
        ShowCurrentCursor();
    }

    public void MoveCursorRight()
    {
        if (_shouldEarthBeRotating) return;
        _cursorIndex++;
        if (_cursorIndex >= _cursors.Length)
        {
            _cursorIndex = 0;
            RotationRequired?.Invoke(1);
            _shouldEarthBeRotating = true;
            Invoke(nameof(CancelEarthRotation_Delay), TimeLibrary.EarthRotateTime);
        }
        ShowCurrentCursor();
    }

    private void CancelEarthRotation_Delay()
    {
        _shouldEarthBeRotating = false;
    }
}
