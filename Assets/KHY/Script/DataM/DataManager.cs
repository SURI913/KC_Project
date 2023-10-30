using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using AllUnit;

public class DataManager : MonoBehaviour
{

    const string URL1 = "https://docs.google.com/spreadsheets/d/1MxQdJ3VPN5cg4iqmUdBumdOnqWLzNWSa2QRjQHy_-00/export?format=tsv&range=A2:F";
    //���� M_D01
    const string URL2 = "https://docs.google.com/spreadsheets/d/1MxQdJ3VPN5cg4iqmUdBumdOnqWLzNWSa2QRjQHy_-00/export?format=tsv&gid=1741337337";
    //���� M_D02


    void Start()
    {
        StartCoroutine(Download());
    }

    IEnumerator Download()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL1);
        yield return www.SendWebRequest();

        setData(www.downloadHandler.text);// ������ ��� 
    }


    [SerializeField]
    MonsterData monsterdata;
    void setData(string tsv)
    {
        
                string[] row = tsv.Split('\n'); //���ڿ��� ������ �ٹٲ� 
                int rowSize = row.Length; //ũ������ 
                int columnSize = row[0].Split('\t').Length;// ù��° �࿡�� �� �Ͽ� ���� ũ�� ���� 

            /*    monsterdata.monsterdatas = new MonsterD[rowSize];

                for (int i = 0; i < rowSize; i++)
                {
                    string[] column = row[i].Split('\t');
                    if (column.Length < 6)
                    {
                        Debug.LogError("Invalid data in row " + (i + 2)); // Row index starts from 0, but Google Sheets starts from 2.
                        continue;
                    }

                    MonsterD monsdata = new MonsterD();
                    monsdata.stageID = column[0];
                    monsdata.hp = double.Parse(column[1]);
                    monsdata.attack = double.Parse(column[2]);
                    monsdata.atktime = int.Parse(column[3]);
                    monsdata.recommattack = double.Parse(column[4]);
                    monsdata.recommdefense = double.Parse(column[5]);

                    // Store the monster data in the array
                    monsterdata.monsterdatas[i] = monsdata;
                }*/
        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            string firstColumn = column[0];
            //  Debug.Log(firstColumn);// ���������̸� ���
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
    }
}

    



/*[System.Serializable]
public class AllData
{
    public MapData[] stageA; // ��Ʈ �̸��� �����ؾ��� ,����
    public MapData[] stageB; // ����
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
