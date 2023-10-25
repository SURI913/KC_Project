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

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            string firstColumn = column[0];
          //  Debug.Log(firstColumn);// ���������̸� ���
            for (int j = 1; j < columnSize; j++)
            {
                MonsterD monsdata = monsterdata.monsterdatas[i];


                monsdata.stageID = firstColumn;
                monsdata.hp = int.Parse(column[1]);
                monsdata.attack = int.Parse(column[2]);
                monsdata.atktime = int.Parse(column[3]);
                monsdata.recommattack = int.Parse(column[4]);
                monsdata.recommdefense = int.Parse(column[5]);

             
                /*
                    if (float.TryParse(column[j], out float floatValue))//string�� float���� ���� 
                    {



                        Debug.Log(Unit.ToUnitString(floatValue));
                        //unit ��ũ��Ʈ�� ���� �����ϰ�
                        //�����ʹ� ������ ���� ��µȴ� 
                        //�ٵ� ��ũ���ͺ� ������Ʈ�� ����
                    }
                    else
                    {
                        // ��ȯ�� ������ ��쿡 ���� ó��
                        Debug.LogError("Parsing failed for: " + column[j]);
                    }*/

            }
        }
    }
}
       /*  public TextAsset stageA; //json ���� (��Ʈ ����) �ν����Ϳ��� �ֱ� 
         public TextAsset stageB;
         private AllData datas;

         private void Awake()
         {
             //�� ����� ��µǴ��� Ȯ�� 
             datas = JsonUtility.FromJson<AllData>(stageA.text);
                 foreach (var VARIABLE in datas.stageA)
                 {
                     Debug.Log(("���̵�" + VARIABLE.stageID));
                     Debug.Log("ü��" + Unit.ToUnitString(VARIABLE.HP));
                     Debug.Log("���ݷ�" + Unit.ToUnitString(VARIABLE.Attack));
                     Debug.Log("������Ž" + Unit.ToUnitString(VARIABLE.atkTime));
                     Debug.Log("��õ ���ݷ�" + Unit.ToUnitString(VARIABLE.RecommendedAttack));
                     Debug.Log("��õ ����" + Unit.ToUnitString(VARIABLE.RecommendedDefense));
                     //�����̰� § unit ���� ������
                 }

            

         }*/


    



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
