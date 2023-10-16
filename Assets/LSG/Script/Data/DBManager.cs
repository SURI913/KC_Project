using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    const string CannonURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=1782858807&range=C8:J";
    const string RepairmanURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=575324561&range=C11:J";

    private void Awake()
    {
        StartCoroutine(DownloadCannon());
        StartCoroutine(DownloadRepairMan());

    }

    public IEnumerator DownloadCannon()
    {
        //캐논 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(CannonURL);
        yield return www.SendWebRequest();
        SetCannonData(www.downloadHandler.text);
    }

    public IEnumerator DownloadRepairMan()
    {
        //캐논 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(RepairmanURL);
        yield return www.SendWebRequest();
        SetRepairManData(www.downloadHandler.text);
    }

    [SerializeField] TowerData towerData;
    void SetCannonData(string tvc)
    {
        string[] row = tvc.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;

        for(int i = 0; i< rowSize; i++)
        {
            string[] column = row[i].Split("\t");
            for(int j = 0; j< columnSize; j++)
            {
                CannonData targetData = towerData.Cannon[i];

                targetData.ID = column[0];
                targetData.Tier = column[1];
                targetData.RetentionAttack = double.Parse(column[2]);
                targetData.RetentionIncrease = double.Parse(column[3]);
                targetData.Attack = double.Parse(column[6]);
                targetData.Increase = double.Parse(column[7]);
            }
        }
    }

    void SetRepairManData(string tvc)
    {
        string[] row = tvc.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split("\t");
            for (int j = 0; j < columnSize; j++)
            {
                RepairManData targetData = towerData.RepairMan[i];

                targetData.ID = column[0];
                targetData.Tier = column[1];
                targetData.RetentionHp = double.Parse(column[2]);
                targetData.RetentionIncrease = double.Parse(column[3]);
                targetData.Hp = double.Parse(column[6]);
                targetData.Increase = double.Parse(column[7]);
            }
        }
    }
}
