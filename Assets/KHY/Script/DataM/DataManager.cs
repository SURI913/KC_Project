using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using AllUnit;

public class DataManager : MonoBehaviour
{

    const string M_D01 = "https://docs.google.com/spreadsheets/d/1MxQdJ3VPN5cg4iqmUdBumdOnqWLzNWSa2QRjQHy_-00/export?format=tsv&gid=0&range=A2:F";
    //몬스터 M_D01
    const string M_D02 = "https://docs.google.com/spreadsheets/d/1MxQdJ3VPN5cg4iqmUdBumdOnqWLzNWSa2QRjQHy_-00/export?format=tsv&gid=1741337337";
    //보스 M_D02


    void Start()
    {
        StartCoroutine(Download());
    }

    IEnumerator Download()
    {
        UnityWebRequest www = UnityWebRequest.Get(M_D01);
        yield return www.SendWebRequest();

        D_setData(www.downloadHandler.text);// 데이터 출력 
    }
    [SerializeField]
    MonsterData dungeon_monsterData;

    void D_setData(string tsv)
    {
        Debug.Log("monsterData: " + dungeon_monsterData);  // 확인
        if (dungeon_monsterData != null)
        {
            Debug.Log("monsterData.monsterdatas: " + dungeon_monsterData.dungeon_monsterdatas);  // 확인
        }
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            // ScriptableObject에서 데이터 가져오기
            MonsterD monsdata = dungeon_monsterData.dungeon_monsterdatas[i];

            monsdata.dungeon_monster_stageID = column[0];
            monsdata.dungeon_monster_hp = double.Parse(column[1]);
            monsdata.dungeon_monster_attack = double.Parse(column[2]);
            monsdata.dungeon_monster_atktime = int.Parse(column[3]);
            monsdata.dungeon_monster_recommattack = double.Parse(column[4]);
            monsdata.dungeon_monster_recommdefense = double.Parse(column[5]);
        }
    }
}

    



