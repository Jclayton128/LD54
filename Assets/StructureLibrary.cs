using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureLibrary : MonoBehaviour
{
    public enum Structures { EmptyLot,
        Crater, House_Basic, Farm_Basic, Mine_Basic, Lab_Basic, Turret_Basic,
        House_Middle_Cap, House_Middle_B, House_Middle_C,
        Farm_Mid_Growth, Farm_Mid_Yield, Farm_High_Growth,
        Mine_Mid_Auto, Mine_Mid_Storage, Mine_Mid_Rate,
        Lab_Mid, Lab_Mid_Damage, Lab_Mid_Growth,
        Turret_Quick, Turret_LongRange, Turret_Heavy,
        House_High_Cap, House_Super_Cap, Farm_High_Yield,
        Lab_High, Mine_High_Storage, Mine_High_Rate,
        Turret_MegaHeavy

        };

    public static StructureLibrary Instance { get; private set; }

    [SerializeField] List<StructureHandler> _structPrefabs = new List<StructureHandler>();

    //state
    Dictionary<Structures, GameObject> _structureMenu = new Dictionary<Structures, GameObject>();

    private void Awake()
    {
        Instance = this;
        AssembleDictionary();
    }

    private void AssembleDictionary()
    {
        foreach (var prefab in _structPrefabs)
        {
            if (!_structureMenu.ContainsKey(prefab.StructureType))
            {
                _structureMenu.Add(prefab.StructureType, prefab.gameObject);
            }
            else
            {
                Debug.LogWarning($"Menu already contains a {prefab.StructureType}");
            }
        }
    }

    public GameObject GetPrefabFromMenu(Structures structure)
    {
        if (!_structureMenu.ContainsKey(structure))
        {
            Debug.LogWarning($"menu doesn't contain a {structure}");
        }
        return _structureMenu[structure];
    }

    public StructureBrochure GetBrochureFromMenu(Structures structure)
    {
        if (!_structureMenu.ContainsKey(structure))
        {
            Debug.LogWarning($"menu doesn't contain a {structure}");
        }
        return _structureMenu[structure].GetComponent<StructureBrochure>();
    }
}
