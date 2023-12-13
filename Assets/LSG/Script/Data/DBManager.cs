using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    const string CannonURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=1782858807&range=B15:L";
    const string RepairmanURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=575324561&range=B11:L";
    const string GrowthAtkURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=2084063042&range=I38:O";
    const string GrowthHpURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=2084063042&range=P38:V";
    const string GrowthProtectionURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=2084063042&range=W38:AC";
    const string GrowthHealingURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=2084063042&range=AD38:AJ";

    private void Awake()
    {
        StartCoroutine(DownloadCannon());
        StartCoroutine(DownloadRepairMan());
        StartCoroutine(DownloadGrowthAtk());
        StartCoroutine(DownloadGrowthHp());
        StartCoroutine(DownloadGrowthProtection());
        StartCoroutine(DownloadGrowthHealing());

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

    public IEnumerator DownloadGrowthProtection()
    {
        //캐논 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(GrowthProtectionURL);
        yield return www.SendWebRequest();
        SetGrowthProtection(www.downloadHandler.text);
    }

    public IEnumerator DownloadGrowthHealing()
    {
        //캐논 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(GrowthHealingURL);
        yield return www.SendWebRequest();
        SetGrowthHeal(www.downloadHandler.text);
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
                towerData.Cannon[i].retention_attack = double.Parse(column[3]);  //보유효과 공격력
                towerData.Cannon[i].retention_increase_attack = double.Parse(column[4]); //보유효과 증가폭
                towerData.Cannon[i].retention_protection = double.Parse(column[5]); //보유효과 증가폭
                towerData.Cannon[i].retention_increase_protection = double.Parse(column[6]); //보유효과 증가폭
                towerData.Cannon[i].attackX = double.Parse(column[7]);
                towerData.Cannon[i].attackX_increase = double.Parse(column[8]);
                towerData.Cannon[i].protectionX = double.Parse(column[9]);
                towerData.Cannon[i].protectionX_increase = double.Parse(column[10]);
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
                targetData.retention_hp = double.Parse(column[3]);
                targetData.retention_increase = double.Parse(column[4]);
                targetData.retention_healing = double.Parse(column[5]);
                targetData.retention_increase_healing = double.Parse(column[6]);
                targetData.hpX = double.Parse(column[7]);
                targetData.hpX_increase = double.Parse(column[8]);
                targetData.healingX = double.Parse(column[9]);
                targetData.healingX_increase = double.Parse(column[10]);

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
                targetData.sub_curreny_min = double.Parse(column[4]);
                targetData.sub_curreny_max = double.Parse(column[5]);
                targetData.sub_curreny_Increase = double.Parse(column[6]);
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
                targetData.sub_curreny_min = double.Parse(column[4]);
                targetData.sub_curreny_max = double.Parse(column[5]);
                targetData.sub_curreny_Increase = double.Parse(column[6]);
            }
        }
        growingSetData.Hp = growthData.GrowthHp[0].Hp;
    }

    void SetGrowthProtection(string tvc)
    {
        string[] row = tvc.Split('\n');
        int rowSize = 2;  //row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split("\t");
            for (int j = 0; j < columnSize; j++)
            {
                ProtectionData targetData = growthData.growth_protection[i];

                targetData.Lv = int.Parse(column[0]);
                targetData.MaxLV = int.Parse(column[1]);
                targetData.protection = double.Parse(column[2]);
                targetData.Increase = double.Parse(column[3]);
                targetData.sub_curreny_min = double.Parse(column[4]);
                targetData.sub_curreny_max = double.Parse(column[5]);
                targetData.sub_curreny_Increase = double.Parse(column[6]);
            }
        }
        growingSetData.protection = growthData.growth_protection[0].protection;
    }

    void SetGrowthHeal(string tvc)
    {
        string[] row = tvc.Split('\n');
        int rowSize = 2;  //row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split("\t");
            for (int j = 0; j < columnSize; j++)
            {
                HealingData targetData = growthData.growth_heal[i];

                targetData.Lv = int.Parse(column[0]);
                targetData.MaxLV = int.Parse(column[1]);
                targetData.healing = double.Parse(column[2]);
                targetData.Increase = double.Parse(column[3]);
                targetData.sub_curreny_min = double.Parse(column[4]);
                targetData.sub_curreny_max = double.Parse(column[5]);
                targetData.sub_curreny_Increase = double.Parse(column[6]);
            }
        }
        growingSetData.protection = growthData.growth_heal[0].healing;
    }
}
