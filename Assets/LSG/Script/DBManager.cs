using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1aq6Qblifekpz8iy0EvC6DMJ7O1toyHlbXHVuQRclxTk/export?format=tsv&gid=1749143762&range=C11:M";
    
    IEnumerator Start() {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }
}
