using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프래팸들을 보관할 변수들 2
    public GameObject[] prefabs;

    // 리스폰 담당을 하는 리스트들 2
    public List<GameObject>[] pools;

    ObjectPoolManager objectPoolManager;

    void Awake()
    {
        objectPoolManager = ObjectPoolManager.instance;

        if (objectPoolManager == null)
        {
            Debug.LogError("ObjectPoolManager is not assigned or not instantiated properly.");
            return;
        }

        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < prefabs.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        string goName = prefabs[index].name;
        GameObject select = objectPoolManager.GetGo(goName); // ObjectPoolManager의 GetGo() 메서드를 호출하여 오브젝트를 가져옵니다.
        return select;
    }

}
