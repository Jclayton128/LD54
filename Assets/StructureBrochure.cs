using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBrochure : MonoBehaviour
{
    public Sprite Icon => GetComponent<SpriteRenderer>().sprite;
    [SerializeField]string _name = "name";
    public string SName => _name;
    [SerializeField][Multiline(3)] string _desc = "description";
    public string Description => _desc;

    [SerializeField] int _mineralCost = 0;
    public int MineralCost => _mineralCost;
    [SerializeField] int _scienceCost = 0;
    public int ScienceCost => _scienceCost;
}
