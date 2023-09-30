using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer _sr = null;



    public void SetSprite(Sprite sprite)
    {
        _sr.sprite = sprite;
    }

    public void Highlight()
    {
        _sr.color = ColorLibrary.Instance.HighlightedStructure;
    }

    public void Lowlight()
    {
        _sr.color = ColorLibrary.Instance.LowlightedStructure;
    }
}
