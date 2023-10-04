using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] double Currency;
    void Start()
    {
       Currency = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        Currency+=10000000000000000000;
        Debug.Log(Unit.ToUnitString(Currency));
    }
}
