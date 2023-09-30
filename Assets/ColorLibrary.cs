using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLibrary : MonoBehaviour
{
    public static ColorLibrary Instance { get; private set; }

    //settings
    public Color HighlightedStructure = Color.white;
    public Color LowlightedStructure = Color.grey;

    private void Awake()
    {
        
    Instance = this;
    }
}
