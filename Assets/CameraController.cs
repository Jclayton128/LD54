using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] Transform _creditsLocation = null;
    [SerializeField] Transform _titleLocation = null;
    [SerializeField] Transform _gameLocation = null;
    [SerializeField] Transform _endLocation = null;
    [SerializeField] GameObject _cameraMouse = null;

    //state
    Tween _cameraMoveTween;
    
    private void Awake()
    {
        Instance = this;
    }

    [ContextMenu("Float to Credits")]
    public void FloatToCredits()
    {
        _cameraMoveTween.Kill();
        _cameraMoveTween = _cameraMouse.transform.DOMove(_creditsLocation.position, TimeLibrary.Instance.CameraMoveTime);
    }

    [ContextMenu("Float to Game ")]
    public void FloatToGame()
    {
        _cameraMoveTween.Kill();
        _cameraMoveTween = _cameraMouse.transform.DOMove(_gameLocation.position, TimeLibrary.Instance.CameraMoveTime);
    }

    [ContextMenu("Float to End")]
    public void FloatToEnd()
    {
        _cameraMoveTween.Kill();
        _cameraMoveTween = _cameraMouse.transform.DOMove(_endLocation.position, TimeLibrary.Instance.CameraMoveTime);
    }

    [ContextMenu("Float to Title")]
    public void FloatToTitle()
    {
        _cameraMoveTween.Kill();
        _cameraMoveTween = _cameraMouse.transform.DOMove(_titleLocation.position, TimeLibrary.Instance.CameraMoveTime);
    }

    public void SnapToTitle()
    {
        _cameraMoveTween.Kill();
        _cameraMouse.transform.position = _titleLocation.position;
    }
}
