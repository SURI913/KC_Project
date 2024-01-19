using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Data_Manager : MonoBehaviour
{

    const string URL1 = "https://docs.google.com/spreadsheets/d/1pGQEPMQpuhJJxnWQrZPIvcv1lFWDBfbZ7-6H0LSaWvY/edit?usp=sharing";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Download());
    }

    public IEnumerator Download()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL1);
        yield return www.SendWebRequest();
        SetEnemyData(www.downloadHandler.text);
    }
    void SetEnemyData(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

    }

}
