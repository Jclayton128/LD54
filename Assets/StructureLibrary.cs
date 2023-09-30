using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureLibrary : MonoBehaviour
{
    public enum Structures { EmptyLot,
        Crater, House_Basic, Farm_Basic, Mine_Basic, Lab_Basic, Turret_Basic,
        House_Middle_A, House_Middle_B, House_Middle_C,
        Farm_Middle_A, Farm_Middle_B, Farm_Middle_C,
        Mine_Middle_A, Mine_Middle_B, Mine_Middle_C,
        Lab_Middle_A, Lab_Middle_B, Lab_Middle_C,
        Turret_Middle_A, Turret_Middle_B, Turret_Middle_C
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
