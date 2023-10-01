using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechController : MonoBehaviour
{
    public static TechController Instance { get; private set; }


    //state
    List<StructureLibrary.Structures> _researchedStructures = new List<StructureLibrary.Structures>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameController.Instance.EnterGameMode += HandleNewGame;
    }

    private void HandleNewGame()
    {
        _researchedStructures.Clear();
    }

    public void ResearchNewStructure(StructureLibrary.Structures newStructure)
    {
        _researchedStructures.Add(newStructure);
    }

    public bool CheckIfStructureIsAlreadyKnown(StructureLibrary.Structures testStructure)
    {
        if (_researchedStructures.Contains(testStructure)) return true;
        else return false;
    }
}
