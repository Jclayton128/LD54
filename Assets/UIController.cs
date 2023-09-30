using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    enum Mode { Title, Rotate, Upgrade, Credits}

    public Action<int> EarthRotationRequired;
    public Action<int> UpgradeRotationRequired;
    public static UIController Instance { get; private set; }

    //scene references
    [SerializeField] CursorDriver[] _cursors = null;
    [SerializeField] PanelDriver[] _panels = null;
    [SerializeField] PanelDriver _upgradePanel = null;
    [SerializeField] RotationHandler _earthRotationHandler = null;

    //state
    bool _shouldEarthBeRotating = false;
    int _cursorIndex;
    [SerializeField] Mode _currentMode;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentMode = Mode.Title;
        SetCurrentMode();
        //_cursorIndex = 1;
        _cursorIndex = 0;
        ShowCurrentCursor();
        InputController.Instance.RotationCommanded += HandleRotationCommanded;
        InputController.Instance.UpDepressed += HandleUpDepressed;
        InputController.Instance.DownDepressed += HandleDownDepressed;
        InputController.Instance.UpReleased += HandleUpReleased;
        InputController.Instance.DownReleased += HandleDownReleased;
        
    }

    private void SetCurrentMode()
    {
        switch (_currentMode)
        {
            case Mode.Title:
                _upgradePanel.ShowHidePanel(false);
                break;

            case Mode.Rotate:
                _upgradePanel.ShowHidePanel(false);
                break;

            case Mode.Upgrade:
                _upgradePanel.ShowHidePanel(true);
                break;

            default: break;
        }
    }

    private void HandleUpDepressed()
    {
        if (_currentMode == Mode.Title)
        {
            //go to credits;
        }
        if (_currentMode == Mode.Upgrade)
        {
            _currentMode = Mode.Rotate;
            UpgradeController.Instance.RequestUpgradeCancellation();
            SetCurrentMode();
        }
        if (_currentMode == Mode.Rotate)
        {
            //go to title?
        }
    }

    private void HandleDownDepressed()
    {
        if (_currentMode == Mode.Title)
        {
            //begin game
            _currentMode = Mode.Rotate;
            SetCurrentMode();
        }
        else if (_currentMode == Mode.Rotate)
        {
            _currentMode = Mode.Upgrade;
            SetCurrentMode();
        }
        else if(_currentMode == Mode.Upgrade)
        {
            UpgradeController.Instance.RequestUpgradeInitiation();
        }
    }

    private void HandleUpReleased()
    {
        if (_currentMode == Mode.Upgrade)
        {
            
        }
    }

    private void HandleDownReleased()
    {
        if (_currentMode == Mode.Upgrade)
        {
            UpgradeController.Instance.RequestUpgradeCancellation();
        }
    }


    private void HandleRotationCommanded(int dir)
    {
        switch (_currentMode)
        {
            case Mode.Rotate:
                if (dir < 0) MoveCursorLeft();
                if (dir > 0) MoveCursorRight();
                break;

            case Mode.Upgrade:
                if (dir < 0) MoveUpgradeLeft();
                if (dir > 0) MoveUpgradeRight();
                break;

            default: break;


        }

    }

    private void MoveUpgradeLeft()
    {
        UpgradeController.Instance.MoveUpgradeLeft();
    }

    private void MoveUpgradeRight()
    {
        UpgradeController.Instance.MoveUpgradeRight();
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
            _earthRotationHandler.CommandRotation(-1);
            //EarthRotationRequired?.Invoke(-1);
            _shouldEarthBeRotating = true;
            Invoke(nameof(CancelEarthRotation_Delay), TimeLibrary.Instance.RotateTime);
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
            _earthRotationHandler.CommandRotation(1);
            //EarthRotationRequired?.Invoke(1);
            _shouldEarthBeRotating = true;
            Invoke(nameof(CancelEarthRotation_Delay), TimeLibrary.Instance.RotateTime);
        }
        ShowCurrentCursor();
    }

    private void CancelEarthRotation_Delay()
    {
        _shouldEarthBeRotating = false;
    }

}
