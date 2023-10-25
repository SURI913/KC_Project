using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using AllUnit;

public class DataManager : MonoBehaviour
{

    const string URL1 = "https://docs.google.com/spreadsheets/d/1MxQdJ3VPN5cg4iqmUdBumdOnqWLzNWSa2QRjQHy_-00/export?format=tsv&range=A2:F";
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
          //  Debug.Log(firstColumn);// 스테이지이름 출력
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
                    if (float.TryParse(column[j], out float floatValue))//string을 float으로 변경 
                    {



                        Debug.Log(Unit.ToUnitString(floatValue));
                        //unit 스크립트로 숫자 간결하게
                        //데이터는 행으로 열로 출력된다 
                        //근데 스크립터블 오브젝트가 뭐임
                    }
                    else
                    {
                        // 변환에 실패한 경우에 대한 처리
                        Debug.LogError("Parsing failed for: " + column[j]);
                    }*/

            }
        }
    }
}
       /*  public TextAsset stageA; //json 파일 (시트 파일) 인스펙터에서 넣기 
         public TextAsset stageB;
         private AllData datas;

         private void Awake()
         {
             //값 제대로 출력되는지 확인 
             datas = JsonUtility.FromJson<AllData>(stageA.text);
                 foreach (var VARIABLE in datas.stageA)
                 {
                     Debug.Log(("아이디" + VARIABLE.stageID));
                     Debug.Log("체력" + Unit.ToUnitString(VARIABLE.HP));
                     Debug.Log("공격력" + Unit.ToUnitString(VARIABLE.Attack));
                     Debug.Log("공격쿨탐" + Unit.ToUnitString(VARIABLE.atkTime));
                     Debug.Log("추천 공격력" + Unit.ToUnitString(VARIABLE.RecommendedAttack));
                     Debug.Log("추천 방어력" + Unit.ToUnitString(VARIABLE.RecommendedDefense));
                     //수경이가 짠 unit 에서 가저옴
                 }

            

         }*/


    



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
