using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeImpact : PoolAble
{

    public ParticleSystem particle_object; //파티클시스템

    public AttackableImp my_attack_data { get; set; }
    public SkillUserImp my_skill_data { get; set; }
    public Transform init_transform { get; set; }

    
    bool is_loop = false; //true = attack, false = skill
    public void  MyHitData(AttackableImp my_data)
    {
        if(my_data != null)
        {
            is_loop = true;
            transform.position = my_data.my_attack_transform.position;
        }
    }
    public void MyHitData(SkillUserImp my_data)
    {
        if (my_data != null)
        {
            is_loop = false;
            transform.position = my_data.my_attack_transform.position;

        }
    }

    private void Awake()
    {
        particle_object = GetComponent<ParticleSystem>();
    }

    //근거리 공격 바로 생성
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.layer == 6 && !is_trigger)
        {
            AttackEft(collision);
            is_trigger = true;
        }
        
    }

    void AttackEft(Collider2D collision)
    {
        DamageableImp target = collision.GetComponent<DamageableImp>();
        if (target != null)
        {
            Debug.Log(my_attack_data.OnAttack(collision));
            //공격이냐 스킬이냐 판별
            if (is_loop) { target.OnDamage(my_attack_data.OnAttack(collision)); }
            else { target.OnDamage(my_skill_data.OnSkill(collision)); }
            //Debug.Log(name + "공격 나갔습니다."+ collision.name +"이 맞았습니다.");
            ReleaseObject();
        }
        

    }
    private void OnEnable()
    {
        if (particle_object != null)
            particle_object.Play();
        is_trigger = false;
    }
}
