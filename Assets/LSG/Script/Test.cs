using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    void Start()
    {
        Renderer myRenderer = GetComponent<Renderer>();
        float width = myRenderer.bounds.size.x;

        Debug.Log(width + "길이");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
