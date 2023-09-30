using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] Transform _creditsLocation = null;
    [SerializeField] Transform _titleLocation = null;
    [SerializeField] Transform _gameLocation = null;
    [SerializeField] Transform _endLocation = null;
    [SerializeField] GameObject _cameraMouse = null;
    [SerializeField] float _normalZoom = 60f;
    [SerializeField] float _attackZoom = 100f;


    //state
    Tween _cameraMoveTween;
    Tween _cameraZoomTween;
    CinemachineVirtualCamera _cvc;
    float _fov;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _cvc = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        _fov = _normalZoom;
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

    public void SetAttackZoom()
    {
        _cameraZoomTween.Kill();
        _fov = _cvc.m_Lens.FieldOfView;
        // Tween a float called myFloat to 52 in 1 second
        _cameraZoomTween = DOTween.To(() => _fov, x => _fov = x, _attackZoom, TimeLibrary.Instance.CameraZoomTime);

    }

    public void SetNormalZoom()
    {
        _cameraZoomTween.Kill();
        _fov = _cvc.m_Lens.FieldOfView;
        _cameraZoomTween = DOTween.To(() => _fov, x => _fov = x, _normalZoom, TimeLibrary.Instance.CameraZoomTime);
    }

    private void Update()
    {
        _cvc.m_Lens.FieldOfView = _fov;
    }
}
