using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Data_Manager : MonoBehaviour
{
    string Enemy_BASE_URL = "https://docs.google.com/spreadsheets/d/1pGQEPMQpuhJJxnWQrZPIvcv1lFWDBfbZ7-6H0LSaWvY/export?format=tsv&gid=1772433253&range=";

    public Enemy_Data enemyData;

    void Start()
    {
        StartCoroutine(DownloadAll());
    }

    IEnumerator DownloadAll()
    {
        yield return DownloadAndSetData(Enemy_BASE_URL + "D18:E", SetM_N11Data);
        yield return DownloadAndSetData(Enemy_BASE_URL + "F17:G", SetM_N12Data);
        yield return DownloadAndSetData(Enemy_BASE_URL + "H17:I", SetM_N13Data);
        yield return DownloadAndSetData(Enemy_BASE_URL + "J17:K", SetM_N14Data);
    }

    IEnumerator DownloadAndSetData(string url, System.Action<string, List<Enemy>> setDataFunction)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        List<Enemy> enemyList = new List<Enemy>();
        setDataFunction(www.downloadHandler.text, enemyList);
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
}