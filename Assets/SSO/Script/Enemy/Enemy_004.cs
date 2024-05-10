using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Spine.Unity;
using DamageNumbersPro.Demo;
using DamageNumbersPro;

public class Enemy_004 : PoolAble, DamageableImp
{
    // 공중형 원거리 몬스터
    // private
    private float enemySpeed;    // 몬스터 이동속도
    private double hp;                  // 몬스터 체력
    private float damage;             // 몬스터의 데미지
    private Transform target;                       // 타겟
    private float originalEnemySpeed;        // 공격이 끝난 후 다시 움직일때 할당할 이동값
    private Coroutine attackCoroutine;               // 코루틴이 여러번 겹치지 않게할 변수
    private bool isAttack = true;                          
    private Animator enemyAnimation; // Unity Animation 컴포넌트
    [SerializeField]
    private Enemy_Respown enemyRespawner;  // 참조

    // public
    public float attackCooldown;  // 공격 쿨타임
    private float rayLength;           // 레이캐스트의 길이
    public float minRayLength = 9f; // 최소 랜덤 값
    public float maxRayLength = 10f; // 최대 랜덤 값
    public GameObject enemy_attack_4;   // 공격시 소환할 공격개체
    public GameObject damagePrefab;

    void Start()
    {
        // 만약 enemyRespawner가 설정되지 않았다면, 현재 씬에서 Enemy_Respown 인스턴스를 찾아 설정
        // 이렇게 하면 에디터에서 수동으로 설정하지 않아도 자동으로 참조를 찾아줌
        if (!enemyRespawner)
        {
            enemyRespawner = FindObjectOfType<Enemy_Respown>();
        }

        transform.position = enemyRespawner.GetFlyEnemyPosition();
        target = GameObject.FindGameObjectWithTag("Castle").transform;
        enemySpeed = enemyRespawner.GetEnemySpeed();
        originalEnemySpeed = enemySpeed;
        hp = enemyRespawner.GetEnemy4Hp();

        enemyAnimation = GetComponent<Animator>();

        // enemy가 소환될 때 랜덤한 rayLength 값 설정
        rayLength = Random.Range(minRayLength, maxRayLength);

        //Debug.Log("enemy4 ray = " + rayLength);
    }

    public void OnDamage(double Damage)
    {
        hp -= Damage;
        DisplayDamageNumber(Damage);
        //Debug.Log(gameObject.name + "이" + Damage + "만큼 데미지를 입었습니다.");
        if (hp <= 0)
         {
            //Destroy(gameObject);
            DeadAnimation();
            
            Debug.Log(gameObject.name + "처치");
        }
    }

    IEnumerator DeadAnimation()
    {
        enemyAnimation.ResetTrigger("attack");
        enemyAnimation.SetTrigger("dead");
        yield return new WaitForSeconds(1.5f);
        ReleaseObject();
    }

    void DisplayDamageNumber(double Damage)
    {
        DamageNumber prefab;
        prefab = damagePrefab.GetComponent<DamageNumber>();

        DNP_PrefabSettings settings = DNP_DemoManager.instance.GetSettings();

        // 생성된 데미지 숫자에 데미지 및 설정을 적용
        DamageNumber newDamageNumber = prefab.Spawn(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), (float)Damage);
        newDamageNumber.SetFollowedTarget(transform);

        // 설정 적용
        settings.Apply(newDamageNumber);
    }

    void Update()
    {
        enemyAnimation.SetTrigger("walk");
        transform.Translate(Vector2.left * Time.deltaTime * enemySpeed);

        // Raycast를 사용하여 "Castle" 또는 "Player"를 감지
        Vector2 raycastStartPosition = new Vector2(transform.position.x, transform.position.y + 1);
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPosition, Vector2.left, rayLength, LayerMask.GetMask("Castle", "Player"));

        // Ray를 시각적으로 표시
        Debug.DrawRay(raycastStartPosition, Vector2.left * rayLength, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Castle") || hit.collider.CompareTag("Player"))
            {
                enemyAnimation.ResetTrigger("walk");
                enemySpeed = 0;

                // 공격 플래그가 true인 경우에만 공격 코루틴을 시작
                if (isAttack)
                {
                    // 이전에 실행 중이던 Attack 코루틴을 중지
                    if (attackCoroutine != null)
                    {
                        StopCoroutine(attackCoroutine);
                    }

                    // Attack 코루틴을 시작
                    attackCoroutine = StartCoroutine(Attack());
                    isAttack = false; // 공격 코루틴을 한 번 시작하면 플래그를 false로 변경
                }
            }
        }
        else
        {
            enemyAnimation.ResetTrigger("walk");
            enemySpeed = originalEnemySpeed;  // 다시 이동
            isAttack = true;
        }

        if (transform.position.x < -20)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            //enemyAnimation.SetTrigger("Enemy_attack");
            enemyAnimation.SetTrigger("attack");
            yield return new WaitForSeconds(0.6f);
            Vector3 spawnPosition = transform.position - Vector3.right + (Vector3.up * 2);
            GameObject attackInstance = Instantiate(enemy_attack_4, spawnPosition, Quaternion.identity);

            // 공격 오브젝트를 적 오브젝트의 자식으로 설정
            attackInstance.transform.parent = transform;

            yield return new WaitForSeconds(attackCooldown);

            Destroy(attackInstance);
        }
    }
}