using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class StatsUp : MonoBehaviour, IAttackDamage
{
    public void think()
    {
        //스테이지  및 공격,방어 전투력 리스트 필요 ?
        //어칼지 추천좀 ㅋㅎ 
        // 2차원 배열로 스테이지 뽑아내? 인덱스값.. 임의로 다 설정해야해?
        //공격체크해야함
        //몬스터 무브,프리팹 다시 만들기
        /*for (int i = 0; i < 2; i++)
        {
            //Instantiate(Monster, transform.position, Quaternion.identity);
        }*/
    }

    public string M_ID { get; set; } //몬스터 넘버
    public float M_HP { get; set; } //체략
    public float M_MaxHP { get; set; } //최대체력
    //추천 공격 전투력 /10
    public float M_Attack; //공격력
                         

   //virtual 가상함수 재정의
    public virtual void GetDamage()
    {
        //추천 방어 전투력/100
    }
    public virtual void GetHP()
    {
        //추천 공격 전투력 /10
    }
}
