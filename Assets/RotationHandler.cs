using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationHandler : MonoBehaviour
{
    //settings
    [SerializeField] Vector3 _leftRotate = new Vector3(0, 0, -30);
    [SerializeField] Vector3 _rightRotate = new Vector3(0, 0, 30);

    //state
    bool _isMoving = false;
    Vector3 _targetRotation;
    Tween _rotateTween;


    //private void Start()
    //{
    //    if (_isEarth) UIController.Instance.EarthRotationRequired += HandleRotationCommanded;
    //    else UIController.Instance.EarthRotationRequired -= HandleRotationCommanded;
    //}

    public void CommandRotation(int dir)
    {
        if (_isMoving) return;
        _isMoving = true;
        _rotateTween.Kill();
        Invoke(nameof(EndMovement_Delay), TimeLibrary.Instance.RotateTime);
        if (dir < 0)
        {
            _targetRotation = transform.rotation.eulerAngles + _leftRotate;
            _rotateTween = transform.DORotate(_targetRotation, TimeLibrary.Instance.RotateTime);
        } 
        if (dir > 0)
        {
            _targetRotation = transform.rotation.eulerAngles + _rightRotate;
            _rotateTween = transform.DORotate(_targetRotation, TimeLibrary.Instance.RotateTime);
        }

    }

    private void EndMovement_Delay()
    {
        _isMoving = false;
    }


}
