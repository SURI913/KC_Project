using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolAble : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }
    public bool is_trigger; //충돌 중복 체크, 이미 충돌했다면 다른 오브젝트 충돌하면 안됨

    public void ReleaseObject()
    {
        Pool.Release(this.gameObject);
    }

}
