using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Action<Mode> ModeChanged;
    public enum Mode { Title, Rotate, Upgrade, Credits, End, Attack, Inspection}

    public Action<int> SiteSelectionChanged;
    public Action<int> UpgradeRotationRequired;
    public static UIController Instance { get; private set; }

    //scene references
    [SerializeField] CursorDriver[] _cursors = null;
    [SerializeField] PanelDriver[] _panels = null;
    [SerializeField] PanelDriver _creditsPanel = null;
    [SerializeField] PanelDriver _titlePanel = null;
    [SerializeField] PanelDriver[] _attackPanels = null;
    [SerializeField] PanelDriver[] _rotatePanels = null;
    [SerializeField] PanelDriver[] _upgradePanel = null;
    [SerializeField] PanelDriver _endPanel = null;
    [SerializeField] RotationHandler _earthRotationHandler = null;
    [SerializeField] SpriteRenderer _earth = null;
    [SerializeField] RotationHandler _endRotationHandler = null;
    [SerializeField] EndPanelDriver _endPanelDriver = null;

    //state
    bool _shouldEarthBeRotating = false;
    [SerializeField] bool _canRotate = false;

    int _cursorIndex;
    [SerializeField] Mode _currentMode;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentMode = Mode.Title;
        CameraController.Instance.SnapToTitle();
        SetPanelsForCurrentMode();

        //_cursorIndex = 1;
        _cursorIndex = 0;
        ShowCurrentCursor();
        InputController.Instance.RotationCommanded += HandleRotationCommanded;
        InputController.Instance.UpDepressed += HandleUpDepressed;
        InputController.Instance.DownDepressed += HandleDownDepressed;
        InputController.Instance.UpReleased += HandleUpReleased;
        InputController.Instance.DownReleased += HandleDownReleased;
        
    }

    public void EnterAttackMode()
    {
        _currentMode = Mode.Attack;
        SetPanelsForCurrentMode();
        CameraController.Instance.SetAttackZoom();
        //_canRotate = false;
        _canRotate = true;
    }

    public void ExitAttackMode()
    {
        _currentMode = Mode.Rotate;
        SetPanelsForCurrentMode();
        CameraController.Instance.SetNormalZoom();
        _canRotate = true;
    }

    public void EnterEndGame()
    {
        _currentMode = Mode.End;
        SetPanelsForCurrentMode();
        CameraController.Instance.SetNormalZoom();
    }

    public void ReturnToRotateUponUpgradeCompletion()
    {
        _currentMode = Mode.Rotate;
        SetPanelsForCurrentMode();
    }

    private void SetPanelsForCurrentMode()
    {
        ModeChanged?.Invoke(_currentMode);
        foreach (var panel in _panels)
        {
            panel.ShowHidePanel(false);
        }
        switch (_currentMode)
        {
            case Mode.Credits:
                _canRotate = false;
                _creditsPanel.ShowHidePanel(true);
                _earth.color = ColorLibrary.Instance.LowlightedEarth;
                break;

            case Mode.Title:
                _canRotate = false;
                _titlePanel.ShowHidePanel(true);
                _earth.color = ColorLibrary.Instance.LowlightedEarth;
                break;

            case Mode.Rotate:
                foreach (var panel in _rotatePanels)
                {
                    panel.ShowHidePanel(true);
                }
                _canRotate = true;
                _earth.color = ColorLibrary.Instance.LowlightedEarth;
                break;

            case Mode.Upgrade:
                _canRotate = true;
                foreach (var panel in _rotatePanels)
                {
                    panel.ShowHidePanel(true);
                }
                foreach (var panel in _upgradePanel)
                {
                    panel.ShowHidePanel(true);
                }
                _earth.color = ColorLibrary.Instance.LowlightedStructure;
                break;

            case Mode.Attack:
                //_canRotate = false;
                foreach (var panel in _attackPanels)
                {
                    panel.ShowHidePanel(true);
                }
                _earth.color = ColorLibrary.Instance.HighlightedStructure;
                break;

            case Mode.End:
                _canRotate = true;
                GameController.Instance.ExitGameMode();
                _endPanelDriver.PopulateSelf();
                _endPanel.ShowHidePanel(true);
                _earth.color = ColorLibrary.Instance.HighlightedStructure;
                break;

            default: break;
        }
    }

    private void HandleUpDepressed()
    {
        if (_currentMode == Mode.Title)
        {
            CameraController.Instance.FloatToCredits();
            _currentMode = Mode.Credits;
            SetPanelsForCurrentMode();
        }
        else if(_currentMode == Mode.Upgrade)
        {
            _currentMode = Mode.Rotate;
            UpgradeController.Instance.RequestUpgradeCancellation();
            SetPanelsForCurrentMode();
        }
        else if(_currentMode == Mode.Rotate)
        {
            //Hold to activate!
            SiteController.Instance.CurrentSite.BeginActivation();
        }
        else if (_currentMode == Mode.End)
        {
            _currentMode = Mode.Title;
            SetPanelsForCurrentMode();
            CameraController.Instance.FloatToTitle();
        }
    }

    private void HandleDownDepressed()
    {
        if (_currentMode == Mode.Credits)
        {
            _currentMode = Mode.Title;
            SetPanelsForCurrentMode();
            CameraController.Instance.FloatToTitle();
        }
        else if (_currentMode == Mode.Title)
        {
            //begin game
            _currentMode = Mode.Rotate;
            SetPanelsForCurrentMode();
            GameController.Instance.StartGameMode();
            CameraController.Instance.FloatToGame();
        }
        else if (_currentMode == Mode.Rotate)
        {
            _currentMode = Mode.Upgrade;
            SetPanelsForCurrentMode();
        }
        else if(_currentMode == Mode.Upgrade)
        {
            UpgradeController.Instance.RequestUpgradeInitiation();
        }
    }

    private void HandleUpReleased()
    {
        if (_currentMode == Mode.Rotate)
        {
            SiteController.Instance.CurrentSite.StopActivation();
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
        if (!_canRotate) return;
        switch (_currentMode)
        {
            case Mode.Attack:
            case Mode.Rotate:
                if (dir < 0) MoveCursorLeft();
                if (dir > 0) MoveCursorRight();
                break;

            case Mode.Upgrade:
                if (dir < 0) MoveUpgradeLeft();
                if (dir > 0) MoveUpgradeRight();
                break;

            case Mode.End:
                if (dir < 0) _endRotationHandler.CommandRotation(-1);
                if (dir > 0) _endRotationHandler.CommandRotation(1);
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
            SiteSelectionChanged?.Invoke(-1);
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
            SiteSelectionChanged?.Invoke(1);
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
