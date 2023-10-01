using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabHelper : MonoBehaviour
{
    [SerializeField] LabHelper _neighborLeft = null;
    [SerializeField] LabHelper _neighborRight = null;
    [SerializeField] HouseHandler _leftHH;
    [SerializeField] HouseHandler _rightHH;
    public float AdjacentPopulation => GetLeftPopulation() + GetRightPopulation();


    private float GetLeftPopulation()
    {
        if (_leftHH == null) _leftHH = _neighborLeft.GetComponentInChildren<HouseHandler>();

        if (_leftHH != null) return _leftHH.Population;
        else return 0;

    }
    private float GetRightPopulation()
    {
        if (_rightHH == null) _rightHH = _neighborRight.GetComponentInChildren<HouseHandler>();

        if (_rightHH != null) return _rightHH.Population;
        else return 0;
    }
}
