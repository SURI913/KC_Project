using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolAble : MonoBehaviour
{
    //총알 때문인것같은데 다시 체크
    public IObjectPool<GameObject> Pool { get; set; }

    public void ReleaseObject()
    {
        Pool.Release(gameObject);
    }
}
