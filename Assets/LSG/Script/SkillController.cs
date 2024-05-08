using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SkillController : MonoBehaviour
{
    //시작과 동시에 데이터 넣어야함
    public SkillData[] init_skill_data;
    private Dictionary<string, SkillData> skill_info = new Dictionary<string, SkillData>();

    GameObject [] my_player;

    void Start()
    {
        //기존 데이터 딕셔너리에 저장해서 사용
        for (int idx = 0; idx < init_skill_data.Length; idx++)
        {
            skill_info.Add(init_skill_data[idx].parent_name, init_skill_data[idx]);
        }

        //플레이어의 스킬 값 받아오기
        my_player = GameObject.FindGameObjectsWithTag("Player");
        for (int idx = 0; idx < my_player.Length; idx++)
        {
            //스킬오브젝트랑 정리해둔 스킬정보가 있으면 해당이미지 가져옴
            if (skill_info.ContainsKey(my_player[idx].name))
            {
                transform.GetChild(idx).GetChild(0).GetComponent<Image>().sprite = skill_info[my_player[idx].name].img;
            }
        }

        //버튼에 함수 연결
        transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(
                    () => AtiveSkill(0));
        transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(
                    () => AtiveSkill(1));
        transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(
                    () => AtiveSkill(2));
    }
    void AtiveSkill(int idx)
    {
        //근거리 공격은 따로 하는중이니 이거를 어케 할지
        my_player[idx].GetComponentInChildren<Attack>().my_attack_type = Attack.AttackType.Skill;
    }
   
}
