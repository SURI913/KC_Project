using DamageNumbersPro.Demo;
using DamageNumbersPro;
using UnityEngine;
using static Attack;

public class Tower : BattleUnit
{
    
    public int lv { get; set; }

    public float attackCooltime = 2f;
    private float attackTime = 0f; 

    private AttackComponent attackComponent;
    private SkillComponent skillComponent;
    private RecoveryComponent recoveryComponent;

    public GameObject targetObject;


    private void Start()
    {
        attackComponent = GetComponentInChildren<AttackComponent>();
    }

    private void Update()
    {
        attackTime += Time.deltaTime;

        if (attackTime >= attackCooltime)
        {
            PerformAttack();
            attackTime = 0f;
        }
    }

    public void PerformAttack()
    {
        if (attackComponent != null)
            attackComponent.Attack(targetObject.transform.position);
    }

    public void PerformSkill()
    {
        if (skillComponent != null)
            skillComponent.UseSkill();
    }

    public void Recovery()
    {
        if (recoveryComponent != null)
            recoveryComponent.Recover();
    }
}
