using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarHandler : MonoBehaviour
{
    SpriteRenderer _sr;

    //state
    Color col = Color.white;
    float _brightness;
    
    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();

        _brightness = UnityEngine.Random.Range(0.5f, 1f);

        float randDepth = UnityEngine.Random.Range(2, 5);
        Vector3 newpos = new Vector3(0, 0, randDepth);
        transform.position += newpos;
        col.a = _brightness;
        _sr.color = col;
    }
}
