using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public SpriteRenderer[] floor = new SpriteRenderer[3]; // 크기 3의 배열로 변경
    public Sprite[] sky;
    SpriteRenderer temp;
    public float speed;
    void Start()
    {
        temp = floor[0];
    }

    void Update()
    {
        for (int i = 0; i < floor.Length; i++)
        {
            if (-39 >= floor[i].transform.position.x)
            {
                for (int q = 0; q < floor.Length; q++)
                {
                    if (temp.transform.position.x < floor[q].transform.position.x)
                        temp = floor[q];
                }
                floor[i].transform.position = new Vector2((float)(temp.transform.position.x + 38.3), 0.0f);
                floor[i].sprite = sky[Random.Range(0, sky.Length)];
            }
        }
        for (int i = 0; i < floor.Length; i++)
        {
            floor[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
        }
    }
}
