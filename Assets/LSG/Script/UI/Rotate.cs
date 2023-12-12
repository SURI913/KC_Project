using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    private RectTransform rotation_target;
    private float speed = -1;

    void Start()
    {
        Init_Rotate();
    }

    private void Init_Rotate()
    {
        rotation_target = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rotation_target.Rotate(Vector3.forward * speed);
    }
}
