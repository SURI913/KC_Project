using DamageNumbersPro.Demo;
using DamageNumbersPro;
using UnityEngine;

public class Tower : BattleUnit
{
    
    public int lv { get; set; }

    public float attackCooltime = 2f;
    private float attackTime = 0f; 

    private AttackComponent attackComponent;
    private RecoveryComponent recoveryComponent;

    public GameObject targetObject;


    private void Start()
    {
        attackComponent = GetComponentInChildren<AttackComponent>();
        if(myMotion == null ) myMotion = GetComponent<Animator>();
    }

    private void Update()
    {
        attackTime += Time.deltaTime;

        if (attackTime >= attackCooltime)
        {
            myMotion.SetTrigger("isAttack");
            attackTime = 0f;
        }
    }

    public void PerformAttack()
    {
        if (attackComponent != null)
            attackComponent.Attack(targetObject.transform.position);
    }

    public void Recovery()
    {
        if (recoveryComponent != null)
            recoveryComponent.Recover();
    }
}
