using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechController : MonoBehaviour
{
    public static TechController Instance { get; private set; }


    //state
    List<StructureLibrary.Structures> _researchedStructures = new List<StructureLibrary.Structures>();
    public int BonusDamage;
    public float BonusGrowthRate;

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
        BonusDamage = 0;
        BonusGrowthRate = 0;
    }

    public void ModifyBonusDamage(int amount)
    {
        BonusDamage += amount;
    }

    public void ModifyGrowthRate(float amount)
    {
        BonusGrowthRate += amount;
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
