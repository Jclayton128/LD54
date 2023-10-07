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
    ParticleSystem _ps;

    // Start is called before the first frame update
    void Awake()
    {
        _lh = transform.parent.GetComponent<LabHelper>();
        _ps = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        GameController.Instance.EnterGameMode += HandleNewGame;
        if (_addsBonusDamage) TechController.Instance.ModifyBonusDamage(1);
        if (_addsBonusGrowth) TechController.Instance.ModifyGrowthRate(0.05f);
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
        //_scienceStep += _scienceRate * TimeController.Instance.ProductionDeltaTime *
        //    _lh.AdjacentPopulation;
        int rand = UnityEngine.Random.Range(0, 2);
        if (rand == 0) return;
        _scienceStep += _scienceRate * TimeController.Instance.ProductionDeltaTime *
            ResourceController.Instance.CurrentPopulation;
        if (_scienceStep > 1)
        {
            _scienceStep = 0;
            //TODO particle effect
            _ps.Emit(1);
            ResourceController.Instance.SpendScience(-1);
        }
    }

}
