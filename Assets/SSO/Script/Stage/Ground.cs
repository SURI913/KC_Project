using UnityEngine;

public class Ground : MonoBehaviour
{
    public SpriteRenderer[] floor;
    public Sprite[] ground;
    SpriteRenderer temp;
    public float speed;
    private float totalLength = 70f;

    void Start()
    {
        temp = floor[0];
    }

    void Update()
    {
        for (int i = 0; i < floor.Length; i++)
        {
            if (-totalLength >= floor[i].transform.position.x)
            {
                for (int q = 0; q < floor.Length; q++)
                {
                    if (temp.transform.position.x < floor[q].transform.position.x)
                        temp = floor[q];
                }
                floor[i].transform.position = new Vector2((float)(temp.transform.position.x + 65), -0.1f);
                floor[i].sprite = ground[Random.Range(0, ground.Length)];
            }
        }
        for (int i = 0; i < floor.Length; i++)
        {
            floor[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
        }
    }
}
