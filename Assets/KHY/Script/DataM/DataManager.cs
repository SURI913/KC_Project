using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using AllUnit;

public class DataManager : MonoBehaviour
{

    const string URL1 = "https://docs.google.com/spreadsheets/d/1MxQdJ3VPN5cg4iqmUdBumdOnqWLzNWSa2QRjQHy_-00/export?format=tsv&gid=0&range=A2:F";
    //몬스터 M_D01
    const string URL2 = "https://docs.google.com/spreadsheets/d/1MxQdJ3VPN5cg4iqmUdBumdOnqWLzNWSa2QRjQHy_-00/export?format=tsv&gid=1741337337";
    //보스 M_D02


    void Start()
    {
        StartCoroutine(Download());
    }

    IEnumerator Download()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL1);
        yield return www.SendWebRequest();

        setData(www.downloadHandler.text);// 데이터 출력 
    }
    [SerializeField]
    MonsterData monsterData;

    void setData(string tsv)
    {
        Debug.Log("monsterData: " + monsterData);  // 확인
        if (monsterData != null)
        {
            Debug.Log("monsterData.monsterdatas: " + monsterData.monsterdatas);  // 확인
        }
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            // ScriptableObject에서 데이터 가져오기
            MonsterD monsdata = monsterData.monsterdatas[i];

            monsdata.stageID = column[0];
            monsdata.hp = double.Parse(column[1]);
            monsdata.attack = double.Parse(column[2]);
            monsdata.atktime = int.Parse(column[3]);
            monsdata.recommattack = double.Parse(column[4]);
            monsdata.recommdefense = double.Parse(column[5]);
        }
    }
    /*
        [SerializeField]
        MonsterData monsterdata;
        void setData(string tsv)
        {

            string[] row = tsv.Split('\n'); //문자열이 끝나면 줄바꿈 
            int rowSize = row.Length; //크기저장 

            int columnSize = row[0].Split('\t').Length;// 첫번째 행에서 탭 하여 열의 크기 저장 

            for (int i = 0; i < rowSize; i++)
            {
                string[] column = row[i].Split('\t');
                string firstColumn = column[0];

                for (int j = 1; j < columnSize; j++)
                {
                    MonsterD monsdata = monsterdata.monsterdatas[i];


                    monsdata.stageID = firstColumn;
                    monsdata.hp = double.Parse(column[1]);
                    monsdata.attack = double.Parse(column[2]);
                    monsdata.atktime = int.Parse(column[3]);
                    monsdata.recommattack = double.Parse(column[4]);
                    monsdata.recommdefense = double.Parse(column[5]);


                }
            }
        }*/
}

    



/*[System.Serializable]
public class AllData
{
    public MapData[] stageA; // 시트 이름과 동일해야함 ,몬스터
    public MapData[] stageB; // 보스
}
[System.Serializable]
public class MapData
{
    public string stageID;
    public float HP;
    public float Attack;
    public float atkTime;
    public float RecommendedAttack;
    public float RecommendedDefense;

}*/
