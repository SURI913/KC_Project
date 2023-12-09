using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackGround : MonoBehaviour
{
    [SerializeField][Range(0.5f, 20f)] float speed = 3f;

    private float width;
    Vector2 startPos;
    float newPos;

    private void Start()
    {
        Renderer myRenderer = GetComponent<Renderer>();
        width = myRenderer.bounds.size.x;

        startPos = transform.position;
        Debug.Log(width + "길이");
    }

    private void Update()
    {
        newPos = Mathf.Repeat(Time.time * speed, width);
        transform.position = startPos + Vector2.left * newPos;
    }
}
