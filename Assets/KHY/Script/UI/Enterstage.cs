using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enterstage : MonoBehaviour
{
    public GameObject UI;
    public GameObject UI2;
    public CountTime time;
    public void OnClick()
    {
        // SceneManager.LoadScene("Dungeon");
        //�������� ����

        //������ Ŭ�������� setactive�� UI�� ������.
        //ī��Ʈ �����ϱ�
        UI.SetActive(false);
        UI2.SetActive(false);
        time.StartCountdwon();
    }
}

