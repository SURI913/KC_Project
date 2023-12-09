using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    const string CannonURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=1782858807&range=C15:L";
    const string RepairmanURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=575324561&range=B11:J";
    const string GrowthAtkURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=2084063042&range=I38:L";
    const string GrowthHpURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=2084063042&range=N38:Q";

    private void Awake()
    {
        StartCoroutine(DownloadCannon());
        StartCoroutine(DownloadRepairMan());
        StartCoroutine(DownloadGrowthAtk());
        StartCoroutine(DownloadGrowthHp());

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

    public IEnumerator DownloadGrowthAtk()
    {
        //캐논 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(GrowthAtkURL);
        yield return www.SendWebRequest();
        SetGrowthAtk(www.downloadHandler.text);
    }

    public IEnumerator DownloadGrowthHp()
    {
        //캐논 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(GrowthHpURL);
        yield return www.SendWebRequest();
        SetGrowthHp(www.downloadHandler.text);
    }

    [SerializeField] TowerData towerData;
    void SetCannonData(string tvc)
    {
        string[] row = tvc.Split('\n');
        int rowSize = 8;  //row.Length;
        int columnSize = row[0].Split('\t').Length;

        for(int i = 0; i< rowSize; i++)
        {
            string[] column = row[i].Split("\t");
            for(int j = 0; j< columnSize; j++)
            {

                towerData.Cannon[i].ID = column[1];
                towerData.Cannon[i].Tier = column[2];
                towerData.Cannon[i].RetentionAttack = double.Parse(column[3]);
                towerData.Cannon[i].RetentionIncrease = double.Parse(column[4]);
                towerData.Cannon[i].Attack = double.Parse(column[7]);
                towerData.Cannon[i].Increase = double.Parse(column[8]);
                if (i == 8) return;
            }
        }
    }
    [SerializeField] GrowthData growthData;
    [SerializeField] GrowingData growingSetData;

    void SetRepairManData(string tvc)
    {
        string[] row = tvc.Split('\n');
        int rowSize = 8; //row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split("\t");
            for (int j = 0; j < columnSize; j++)
            {
                RepairManData targetData = towerData.RepairMan[i];

                targetData.ID = column[1];
                targetData.Tier = column[2];
                targetData.RetentionHp = double.Parse(column[7]);
                targetData.RetentionIncrease = double.Parse(column[8]);
                targetData.Hp = double.Parse(column[3]);
                targetData.Increase = double.Parse(column[4]);

                if (i == 8) return;
            }
        }
    }

    void SetGrowthAtk(string tvc)
    {
        string[] row = tvc.Split('\n');
        int rowSize = 2;  //row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split("\t");
            for (int j = 0; j < columnSize; j++)
            {
                AttackData targetData = growthData.GrowthAttack[i];

                targetData.Lv = int.Parse(column[0]);
                targetData.MaxLV = int.Parse(column[1]);
                targetData.Attack = double.Parse(column[2]);
                targetData.Increase = double.Parse(column[3]);
            }
        }
        growingSetData.Attack = growthData.GrowthAttack[0].Attack;
    }
    void SetGrowthHp(string tvc)
    {
        string[] row = tvc.Split('\n');
        int rowSize = 2;  //row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split("\t");
            for (int j = 0; j < columnSize; j++)
            {
                HpData targetData = growthData.GrowthHp[i];

                targetData.Lv = int.Parse(column[0]);
                targetData.MaxLV = int.Parse(column[1]);
                targetData.Hp = double.Parse(column[2]);
                targetData.Increase = double.Parse(column[3]);
            }
        }
        growingSetData.Hp = growthData.GrowthHp[0].Hp;
    }
}
