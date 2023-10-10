using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Respown : MonoBehaviour
{
    public PoolManager pool;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3f)
        {
            timer = 0;
            Spawn();
        }
        if (Input.GetKeyDown("space"))
        {
            pool.Get(1);
        }
    }

    void Spawn()
    {
        pool.Get(Random.Range(0, 4));
    }

}
