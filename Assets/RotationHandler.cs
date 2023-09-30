using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationHandler : MonoBehaviour
{
    //settings
    Vector3 _leftRotate = new Vector3(0, 0, -90);
    Vector3 _rightRotate = new Vector3(0, 0, 90);

    //state
    bool _isMoving = false;
    Vector3 _targetRotation;
    Tween _rotateTween;


    private void Start()
    {
        UIController.Instance.RotationRequired += HandleRotationCommanded;
    }

    private void HandleRotationCommanded(int dir)
    {
        if (_isMoving) return;
        _isMoving = true;
        _rotateTween.Kill();
        Invoke(nameof(EndMovement_Delay), TimeLibrary.EarthRotateTime);
        if (dir < 0)
        {
            _targetRotation = transform.rotation.eulerAngles + _leftRotate;
            _rotateTween = transform.DORotate(_targetRotation, TimeLibrary.EarthRotateTime);
        } 
        if (dir > 0)
        {
            _targetRotation = transform.rotation.eulerAngles + _rightRotate;
            _rotateTween = transform.DORotate(_targetRotation, TimeLibrary.EarthRotateTime);
        }

    }

    private void EndMovement_Delay()
    {
        _isMoving = false;
    }


}
