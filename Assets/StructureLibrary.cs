using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureLibrary : MonoBehaviour
{
    public enum Structures { EmptyLot, Crater, House, Farm, Mine, Lab};

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
}
