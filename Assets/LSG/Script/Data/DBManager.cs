using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{

    const string CannonURL = "https://docs.google.com/spreadsheets/d/1pGQEPMQpuhJJxnWQrZPIvcv1lFWDBfbZ7-6H0LSaWvY/export?format=tsv&gid=870727202&range=A3:Q";
    const string RepairmanURL = "https://docs.google.com/spreadsheets/d/1pGQEPMQpuhJJxnWQrZPIvcv1lFWDBfbZ7-6H0LSaWvY/export?format=tsv&gid=173998094&range=A3:Q";
    const string GrowthAtkURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=2084063042&range=I39:O";
    const string GrowthHpURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=2084063042&range=P39:V";
    const string GrowthProtectionURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=2084063042&range=W39:AC";
    const string GrowthHealingURL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=2084063042&range=AD39:AJ";

    const string M_D01 = "https://docs.google.com/spreadsheets/d/1MxQdJ3VPN5cg4iqmUdBumdOnqWLzNWSa2QRjQHy_-00/export?format=tsv&gid=0&range=A2:F";
    //몬스터 M_D01
    const string M_D02 = "https://docs.google.com/spreadsheets/d/1MxQdJ3VPN5cg4iqmUdBumdOnqWLzNWSa2QRjQHy_-00/export?format=tsv&gid=1741337337";
    //보스 M_D02

    string Enemy_BASE_URL = "https://docs.google.com/spreadsheets/d/1pGQEPMQpuhJJxnWQrZPIvcv1lFWDBfbZ7-6H0LSaWvY/export?format=tsv&gid=1772433253&range=";

    [SerializeField] MonsterData dungeon_monsterData;

    [SerializeField] CurrentTowerData current_tower_data;
    [SerializeField] GrowthData growthData;
    [SerializeField] GrowingData growingSetData;
    [SerializeField] TowerData towerData;

    [SerializeField] Enemy_Data enemyData;
    private void Awake()
    {
        StartCoroutine(DownloadEnemyAll());
        StartCoroutine(DownloadCannon());
        StartCoroutine(DownloadRepairMan());
        StartCoroutine(DownloadGrowthAtk());
        StartCoroutine(DownloadGrowthHp());
        StartCoroutine(DownloadGrowthProtection());
        StartCoroutine(DownloadGrowthHealing());
        
        //StartCoroutine(Download());
    } 

    public IEnumerator DownloadCannon()
    {
        //대포 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(CannonURL);
        yield return www.SendWebRequest();
        SetCannonData(www.downloadHandler.text);
    }

    public IEnumerator DownloadRepairMan()
    {
        //수리쥐 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(RepairmanURL);
        yield return www.SendWebRequest();
        SetRepairManData(www.downloadHandler.text);
    }

    public IEnumerator DownloadGrowthAtk()
    {
        //성장 공격 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(GrowthAtkURL);
        yield return www.SendWebRequest();
        SetGrowthAtk(www.downloadHandler.text);
    }

    public IEnumerator DownloadGrowthHp()
    {
        //성장 체력 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(GrowthHpURL);
        yield return www.SendWebRequest();
        SetGrowthHp(www.downloadHandler.text);
    }

    public IEnumerator DownloadGrowthProtection()
    {
        //성장 방어 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(GrowthProtectionURL);
        yield return www.SendWebRequest();
        SetGrowthProtection(www.downloadHandler.text);
    }

    public IEnumerator DownloadGrowthHealing()
    {
        //성장 회복 데이터 가져오기
        UnityWebRequest www = UnityWebRequest.Get(GrowthHealingURL);
        yield return www.SendWebRequest();
        SetGrowthHeal(www.downloadHandler.text);
    }

    IEnumerator DownloadEnemyAll()
    {
        yield return DownloadAndSetEnemyData(Enemy_BASE_URL + "D18:E", SetM_N11Data);
        yield return DownloadAndSetEnemyData(Enemy_BASE_URL + "F17:G", SetM_N12Data);
        yield return DownloadAndSetEnemyData(Enemy_BASE_URL + "H17:I", SetM_N13Data);
        yield return DownloadAndSetEnemyData(Enemy_BASE_URL + "J17:K", SetM_N14Data);
        yield return DownloadAndSetEnemyData(Enemy_BASE_URL + "L17:N", SetBossData);
    }

    IEnumerator DownloadAndSetEnemyData(string url, System.Action<string, List<Enemy>> setDataFunction)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        List<Enemy> enemyList = new List<Enemy>();
        setDataFunction(www.downloadHandler.text, enemyList);
    }

    //==>구분을 위해 이름변경
    IEnumerator Download()
    {
        UnityWebRequest www = UnityWebRequest.Get(M_D01);
        yield return www.SendWebRequest();

        dungeon_monster_setData(www.downloadHandler.text);// 데이터 출력 
    }

    void SetCannonData(string tvc)
    {
        string[] row = tvc.Split('\n');
        int rowSize = 3;  //row.Length;
        int columnSize = row[0].Split('\t').Length;

        for(int i = 0; i< rowSize; i++)
        {
            string[] column = row[i].Split("\t");
            for(int j = 0; j< columnSize; j++)
            {
                CannonData targetData = towerData.Cannon[i];

                targetData.name = column[0];
                targetData.id = column[1];
                targetData.tier = column[2];
                targetData.retention_attack = double.Parse(column[3]);  //보유효과 공격력
                targetData.retention_attack_increase = double.Parse(column[4]); //보유효과 증가폭
                targetData.retention_protection = double.Parse(column[5]); //보유효과 증가폭
                targetData.retention_protection_increase = double.Parse(column[6]); //보유효과 증가폭
                targetData.attackX = double.Parse(column[7]);
                targetData.attackX_increase = double.Parse(column[8]);
                targetData.protectionX = double.Parse(column[9]);
                targetData.protectionX_increase = double.Parse(column[10]);
                targetData.attack_cooltime = float.Parse(column[11]);
                targetData.attack_cooltime_decrease = float.Parse(column[12]);
                targetData.attack_speed = float.Parse(column[13]);
                targetData.attack_speed_increase = float.Parse(column[14]);
                targetData.skill_effect = float.Parse(column[15]);
                targetData.skill_effect_increase = float.Parse(column[16]);
                if (i == 8) return;
            }
        }
        //초기 세팅
        current_tower_data.attackX = towerData.Cannon[0].attackX;
        current_tower_data.protectionX = towerData.Cannon[0].protectionX;

        current_tower_data.retention_attack = towerData.Cannon[0].retention_attack;
        current_tower_data.retention_protection = towerData.Cannon[0].retention_protection;
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

                targetData.name = column[0];
                targetData.id = column[1];
                targetData.tier = column[2];
                targetData.retention_hp = double.Parse(column[3]);
                targetData.retention_hp_increase = double.Parse(column[4]);
                targetData.retention_healing = double.Parse(column[5]);
                targetData.retention_healing_increase = double.Parse(column[6]);
                targetData.hpX = double.Parse(column[7]);
                targetData.hpX_increase = double.Parse(column[8]);
                targetData.healingX = double.Parse(column[9]);
                targetData.healingX_increase = double.Parse(column[10]);
                targetData.attack_cooltime = float.Parse(column[11]);
                targetData.attack_cooltime_decrease = float.Parse(column[12]);
                targetData.attack_speed = float.Parse(column[13]);
                targetData.attack_speed_increase = float.Parse(column[14]);
                targetData.skill_effect = float.Parse(column[15]);
                targetData.skill_effect_increase = float.Parse(column[16]);
            }
        }
        current_tower_data.hpX = towerData.RepairMan[0].hpX;
        current_tower_data.healingX = towerData.RepairMan[0].healingX;

        current_tower_data.retention_hp = towerData.RepairMan[0].retention_hp;
        current_tower_data.retention_healing = towerData.RepairMan[0].retention_healing;
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
        int rowSize = row.Length;
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

    void dungeon_monster_setData(string tsv) //==> 함수이름 수정, 
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

    void SetM_N11Data(string tsv, List<Enemy> enemyList)
    {
        string[] rows = tsv.Split('\n');

        for (int i = 0; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split('\t');

            Enemy enemy = new Enemy();
            enemy.name = "m_n11";
            enemy.hp = double.Parse(columns[0]);
            enemy.damage = float.Parse(columns[1]);

            enemyList.Add(enemy);
        }

        enemyData.enemy1 = enemyList.ToArray();
    }

    void SetM_N12Data(string tsv, List<Enemy> enemyList)
    {
        string[] rows = tsv.Split('\n');

        for (int i = 1; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split('\t');

            Enemy enemy = new Enemy();
            enemy.name = "m_n12";
            enemy.hp = double.Parse(columns[0]);
            enemy.damage = float.Parse(columns[1]);

            enemyList.Add(enemy);
        }

        enemyData.enemy2 = enemyList.ToArray();
    }

    void SetM_N13Data(string tsv, List<Enemy> enemyList)
    {
        string[] rows = tsv.Split('\n');

        for (int i = 1; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split('\t');

            Enemy enemy = new Enemy();
            enemy.name = "m_n13";
            enemy.hp = double.Parse(columns[0]);
            enemy.damage = float.Parse(columns[1]);

            enemyList.Add(enemy);
        }

        enemyData.enemy3 = enemyList.ToArray();
    }

    void SetM_N14Data(string tsv, List<Enemy> enemyList)
    {
        string[] rows = tsv.Split('\n');

        for (int i = 1; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split('\t');

            Enemy enemy = new Enemy();
            enemy.name = "m_n14";
            enemy.hp = double.Parse(columns[0]);
            enemy.damage = float.Parse(columns[1]);

            enemyList.Add(enemy);
        }

        enemyData.enemy4 = enemyList.ToArray();
    }

    void SetBossData(string tsv, List<Enemy> enemyList)
    {
        string[] rows = tsv.Split('\n');

        for (int i = 1; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split('\t');

            Enemy enemy = new Enemy();
            enemy.name = "boss";
            enemy.hp = double.Parse(columns[0]);
            enemy.damage = float.Parse(columns[1]);
            enemy.skill = float.Parse(columns[2]);

            enemyList.Add(enemy);
        }

        enemyData.boss = enemyList.ToArray();
    }
}
