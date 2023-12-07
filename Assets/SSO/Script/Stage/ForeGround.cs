using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeGround : MonoBehaviour
{
    [SerializeField][Range(1f, 20f)] float speed = 3f;

    private float width;
    Vector2 startPos;
    float newPos;

    private void Start()
    {
        Collider2D myCollider = GetComponent<Collider2D>();
        width = myCollider.bounds.size.x;

        startPos = transform.position;
        Debug.Log(width + "길이");
    }

    private void Update()
    {
        newPos = Mathf.Repeat(Time.time * speed, width);
        transform.position = startPos + Vector2.left * newPos;
    }
}
