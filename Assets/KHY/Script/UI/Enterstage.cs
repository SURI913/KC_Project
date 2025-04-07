using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enterstage : MonoBehaviour
{
    public GameObject UI;

    public void OnClick()
    {
      // GameManager.instance.Dungeon_Start_UI(true);
       SceneManager.LoadScene("7");
       Debug.Log("버튼 눌러짐,엔터 ");
    }
}

