using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLibrary : MonoBehaviour
{
    public static ColorLibrary Instance { get; private set; }

    public Color DimResource = Color.grey;
    public Color BrightResource = Color.white;
    public Color HarvestableResource = Color.white;

    //settings
    public Color HighlightedStructure = Color.white;
    public Color LowlightedStructure = Color.grey;
    public Color LowlightedEarth = Color.grey;
    
    public Color HighlightedUpgrade = Color.white;
    public Color LowlightedUpgrade = Color.grey;

    public Color AffordableUpgrade = Color.white;
    public Color UnaffordableUpgrade = Color.red;

    private void Awake()
    {
        
    Instance = this;
    }
}
