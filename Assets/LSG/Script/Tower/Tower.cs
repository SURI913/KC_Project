using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour, IPointerUpHandler
{
    //기본 데이터

    public double hp { get; set; }      //체력   valueType? : null값이 들어가 있으면
    public double maxHp { get; set; }   //최대체력
    protected double attack;  //공격력
    protected double healing = 0; //회복력
    protected bool dead = false;    //죽음확인

    //대포, 수리공 값 인스펙터 입력
    public List<GameObject> Cannon = new List<GameObject>();
    public List<GameObject> Repairman = new List<GameObject>();
    //레벨에 따라 오픈되는 걸 구현 해야하는데우짜면 좋누

    public void OnPointerUp(PointerEventData data) 
    {
        Debug.Log("타워 확인");
        SceneManager.LoadScene("Tower", LoadSceneMode.Additive);
    }
}
