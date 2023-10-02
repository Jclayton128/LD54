using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuePulser : MonoBehaviour
{
    bool _isAscending = true;
    float _dist;
    [SerializeField] float _rate = 0.2f;
    Vector3 _pos = Vector3.up;


    private void Update()
    {
        _dist = transform.localPosition.y;
        if (_isAscending)
        {
            _dist = Mathf.MoveTowards(_dist, 1.1f, _rate * Time.deltaTime);
            if (_dist >= 1.1)
            {
                _isAscending = false;
            }
        }
        else
        {
            _dist = Mathf.MoveTowards(_dist, 1.0f, _rate * Time.deltaTime);
            if (_dist <= 1.0f)
            {
                _isAscending = true;
            }
        }
        _pos.y = _dist;
        transform.localPosition = _pos;
    }
}
