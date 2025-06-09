using DamageNumbersPro.Demo;
using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    //체력, 방어력 관리

    public float health;
    public float maxHealth;
    public float defensePower;
    public float recoveryAmount;
    public bool dead { get; set; } = false;    //죽음확인
    public GameObject damagePrefab;

    //-----------------------------------------------------------애니메이션 
    public Animator myMotion;
    public float respawnTime = 8f;
    protected bool isInvincible;

    public IEnumerator Invincibility(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
        yield break;
    }

    protected void hpInit()
    {    
        //캐릭터 전체 체력

        //체력 초기화
        /*if (!growing_data)
        {
            Debug.Log(cat_data._id);
            Debug.Log("hp error!");
        }
        else
        {
            hp = growing_data.Hp * cat_data._hp_multipler;
            dead = false;
            cat_motion.SetBool("isDead", false);
        }
        //체력이 0보다 작을 경우 초기화가 실행 되어야함*/
    }

    public virtual void TakeDamage(float damage)
    {
        if (!dead && !isInvincible)
        {
            float finalDamage = damage - defensePower;
            health -= finalDamage;
            Debug.Log($"{gameObject.name}이(가) {finalDamage}의 피해를 받음! 남은 체력: {health}");
            DrawDamageNumber(finalDamage);
            if (health <= 0) { Die(); }    //죽음처리
        }
    }

    //데미지 프리펩
    void DrawDamageNumber(float damage)
    {
        DamageNumber prefab;
        prefab = damagePrefab.GetComponent<DamageNumber>();


        DNP_PrefabSettings settings = DNP_DemoManager.instance.GetSettings();

        // 생성된 데미지 숫자에 데미지 및 설정을 적용
        DamageNumber newDamageNumber = prefab.Spawn(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), (float)damage);
        newDamageNumber.SetFollowedTarget(transform);

        // 설정 적용
        settings.Apply(newDamageNumber);
    }

    private void Die()
    {
        dead = true;
        //캐릭터 죽는 모션
        myMotion.SetBool("isDead", true);
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        myMotion.SetTrigger("FinishStune");
        hpInit();
        yield break;
    }

    public void  RecoverHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth); // 체력이 최대치를 넘지 않도록 처리
        Debug.Log($"{gameObject.name}이(가) {amount} 만큼 회복! 현재 체력: {health}");
    }
}
