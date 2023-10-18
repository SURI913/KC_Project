using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �����Ե��� ������ ������ 2
    public GameObject[] prefabs;

    public int enemyCount = 0; // ���� ������ Ƚ���� ����

    // ������ ����� �ϴ� ����Ʈ�� 2
    public List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < prefabs.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ �������� ��Ȱ��ȭ�� ������Ʈ�� �����ϰ�, �װ��� �߰��ϸ� select ������ �Ҵ�
        /*foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf) // ��Ȱ��ȭ�� ������Ʈ�� ã���ϴ�.
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }*/

        // ��Ȱ��ȭ�� ������Ʈ�� ���ٸ� ���Ӱ� �����Ͽ� select�� �Ҵ�
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);

            if (index == 0 || index ==1 || index == 2 || index == 3) // 0 �� �ε����� ��(Enemy)�� ����
            {
                enemyCount++;
                //Debug.Log("enemy��ȯ");
            }
        }

        return select;
    }
}
