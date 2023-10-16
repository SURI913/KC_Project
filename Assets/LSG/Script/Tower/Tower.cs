using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour, IPointerUpHandler
{
    //�⺻ ������

    public double hp { get; set; }      //ü��   valueType? : null���� �� ������
    public double maxHp { get; set; }   //�ִ�ü��
    protected double attack;  //���ݷ�
    protected double healing = 0; //ȸ����
    protected bool dead = false;    //����Ȯ��

    //����, ������ �� �ν����� �Է�
    public List<GameObject> Cannon = new List<GameObject>();
    public List<GameObject> Repairman = new List<GameObject>();
    //������ ���� ���µǴ� �� ���� �ؾ��ϴµ���¥�� ����

    public void OnPointerUp(PointerEventData data) 
    {
        Debug.Log("Ÿ�� Ȯ��");
        SceneManager.LoadScene("Tower", LoadSceneMode.Additive);
    }
}
