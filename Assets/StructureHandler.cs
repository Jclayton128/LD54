using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureHandler : MonoBehaviour
{
    SpriteRenderer _sr;
    public SpriteRenderer SpriteRenderer => _sr;

    [SerializeField] StructureLibrary.Structures _structureType = StructureLibrary.Structures.Crater;
    public StructureLibrary.Structures StructureType => _structureType;
    [SerializeField] List<StructureLibrary.Structures> _upgradeOptions = new List<StructureLibrary.Structures>(5);
    public List<StructureLibrary.Structures> UpgradeOptions => _upgradeOptions;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }
 
}
