using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabHandler : MonoBehaviour
{
    [SerializeField] LabHelper _lh;

    //setting
    [SerializeField] float _scienceRate = 0.01f; // points per second per adj pop
    [SerializeField] bool _addsBonusDamage;
    [SerializeField] bool _addsBonusGrowth;

    //state
    [SerializeField] float _scienceStep;

    // Start is called before the first frame update
    void Awake()
    {
        _lh = transform.parent.GetComponent<LabHelper>();
    }

    private void Start()
    {
        GameController.Instance.EnterGameMode += HandleNewGame;
        if (_addsBonusDamage) TechController.Instance.ModifyBonusDamage(1);
        if (_addsBonusGrowth) TechController.Instance.ModifyGrowthRate(0.005f);
    }

    private void OnDestroy()
    {
        GameController.Instance.EnterGameMode -= HandleNewGame;

        if (_addsBonusDamage) TechController.Instance.ModifyBonusDamage(-1);
        if (_addsBonusGrowth) TechController.Instance.ModifyGrowthRate(-0.01f);
    }

    private void HandleNewGame()
    {
        _scienceStep = 0;
    }

    private void Update()
    {
        _scienceStep += _scienceRate * TimeController.Instance.ProductionDeltaTime *
            _lh.AdjacentPopulation;
        if (_scienceStep > 1)
        {
            _scienceStep = 0;
            //TODO particle effect
            ResourceController.Instance.SpendScience(-1);
        }
    }

}
