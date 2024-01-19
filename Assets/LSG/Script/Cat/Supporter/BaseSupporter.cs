using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSupporter : Cat, AttackableImp, SkillUserImp
{
    public float speed { get; set; }   //공격 속도
    public float atk_time { get; set; } //일반공격 쿨타임
    public float skill_time { get; set; }   //스킬 공격 쿨타임
    public bool is_ative_skill { get; set; } = false;   //스킬 활성화 시 공격 멈춤 보스 및 일반몬스터는 해당 x Awake에서 AiveSkill = false;처리
    
    List<Cat> support_target = new List<Cat>();
    public BaseSupporter(BaseCatData _cat_data, GrowingData _growing_data, GameObject _damage_prefab)
        : base(_cat_data, _growing_data, _damage_prefab)
    { speed = _cat_data._attack_speed; atk_time = _cat_data._atk_time; skill_time = _cat_data._skl_time; skill_effect = _cat_data. _skl_effect; }

    //--------------------------------------------------------------------------------------------------------------공격 : 특수공격이 있으면 override

    private void FindSupportTarget() //대신에 생성자로 값 넣는방식으로 변경해 볼 것
    {
        Vector2 skillpos = this.transform.position;
        //스킬특기
        Collider2D[] cats = Physics2D.OverlapCircleAll(skillpos, 1000.0f);
        foreach (var Cats in cats)
        {
            if (Cats.CompareTag("Player"))
            {
                support_target.Add(Cats.GetComponent<Cat>());
            }
        }

    }

    public double OnSkill(RaycastHit2D hit)
    {
        catMotion.SetTrigger("isSkill");

        //Debug.Log( "힐링스킬 발동");
        //Debug.Log(catsHealing.Count);
        //요새 범위 안에있는 사람한테만 힐이 들어감
        foreach (var Cats in support_target)
        {
            Cats.hp += Cats.growing_data.Hp * cat_data._hp_multipler * skill_effect;
            // Debug.Log(Cats.ID+ "힐링");
            if (hp > growing_data.Hp * cat_data._hp_multipler) //maxHp를 넘지않게 처리
            {
                hp = growing_data.Hp * cat_data._hp_multipler;
            }
        }
        return 0;
    }

    public double OnAttack(RaycastHit2D hit) //공격 체크
    {

        if (hit.collider.CompareTag("boss")) //보스 공격의 경우
        {
            return GetAttackPower() + boss_attack;
        }
        return GetAttackPower();
    }

}
