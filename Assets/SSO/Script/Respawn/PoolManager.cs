using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프래팸들을 보관할 변수들 2
    public GameObject[] prefabs;

    public int enemyCount = 0; // 적이 생성된 횟수를 추적

    // 리스폰 담당을 하는 리스트들 2
    public List<GameObject>[] pools;

    public ObjectPoolManager objectPoolManager;

    void Awake()
    {
        objectPoolManager = ObjectPoolManager.instance;

        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < prefabs.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        //GameObject select = null;
        string goName = prefabs[index].name;
        GameObject select = objectPoolManager.GetGo(goName); // ObjectPoolManager의 GetGo() 메서드를 호출하여 오브젝트를 가져옵니다.

        // 비활성화된 오브젝트가 없다면 새롭게 생성하여 select에 할당
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);

            if (index == 0 || index ==1 || index == 2 || index == 3) // 0 번 인덱스를 적(Enemy)로 가정
            {
                enemyCount++;
                //Debug.Log("enemy소환");
            }
        }
        return select;
    }
}
