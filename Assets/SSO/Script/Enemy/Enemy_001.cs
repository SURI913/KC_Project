using DamageNumbersPro.Demo;
using DamageNumbersPro;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_001 : MonoBehaviour, DamageableImp
{
    // 근거리 몬스터 01
    // private
    private float enemySpeed;    // 몬스터 이동속도
    private double hp;                  // 몬스터 체력
    private float damage;             // 몬스터의 데미지
    private Transform target;                       // 타겟
    private float originalEnemySpeed;        // 공격이 끝난 후 다시 움직일때 할당할 이동값
    private Coroutine attackCoroutine;               // 코루틴이 여러번 겹치지 않게할 변수
    private bool isAttack = true;
    private bool isDead = true;
    private Animator enemyAnimation; // Unity Animation 컴포넌트
    [SerializeField]
    private Enemy_Respown enemyRespawner;  // 참조

    //public
    public float attackCooldown;  // 공격 쿨타임
    public float rayLength;           // 레이캐스트의 길이
    public GameObject enemy_attack_1;   // 공격시 소환할 공격개체
    public GameObject damagePrefab;  // 데미지 프리팹

    void Start()
    {
        // 만약 enemyRespawner가 설정되지 않았다면, 현재 씬에서 Enemy_Respown 인스턴스를 찾아 설정
        // 이렇게 하면 에디터에서 수동으로 설정하지 않아도 자동으로 참조를 찾아줌
        if (!enemyRespawner)
        {
            enemyRespawner = FindObjectOfType<Enemy_Respown>();
        }

        transform.position = enemyRespawner.GetGroundEnemyPosition();
        target = GameObject.FindGameObjectWithTag("Castle").transform;
        enemySpeed = enemyRespawner.GetEnemySpeed();
        originalEnemySpeed = enemySpeed;

        enemyAnimation = GetComponent<Animator>();
    }

    public void OnDamage(double Damage )
    {
        hp -= Damage;
        DisplayDamageNumber(Damage);
        //Debug.Log(gameObject.name + "이" + Damage + "만큼 데미지를 입었습니다.");
        if (hp <= 0)
        {
            Destroy(gameObject); 
            Debug.Log(gameObject.name + "처치");
        }
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

    public void SetStats(double health, float dmg)
    {
        hp = health;
        damage = dmg;
    }

    void Update()
    {
        transform.Translate(Vector2.left* Time.deltaTime * enemySpeed);  // enemy의 이동

        // Raycast를 사용하여 "Castle" 또는 "Player"를 감지
        Vector2 raycastStartPosition = new Vector2(transform.position.x, transform.position.y + 1);
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPosition, Vector2.left, rayLength, LayerMask.GetMask("Castle", "Player"));

        // Ray를 시각적으로 표시
        Debug.DrawRay(raycastStartPosition, Vector2.left * rayLength, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Castle") || hit.collider.CompareTag("Player"))  // target을 찾으면
            {

                enemySpeed = 0;  // 움직임 멈춤

                // 공격 플래그가 true인 경우에만 공격 코루틴을 시작
                if (isAttack)
                {
                    // 이전에 실행 중이던 Attack 코루틴을 중지 (공격이 여러번 나가는걸 방지)
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
        else  // target과 충돌이 없다면,
        {
            enemySpeed = originalEnemySpeed;  // 다시 이동
            isAttack = true;
        }

        if (transform.position.x < -20)  // 맵 밖으로 나가면,
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator Attack()
    {
        while (true) // 무한 반복
        {
            enemyAnimation.SetTrigger("Enemy_attack");
            Vector3 spawnPosition = transform.position - (Vector3.right * 4) + (Vector3.up / 2);
            GameObject attackInstance = Instantiate(enemy_attack_1, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 1.5f));

            // 대기
            yield return new WaitForSeconds(attackCooldown);

            // 발사체 수명이 끝나면 제거
            Destroy(attackInstance);
        }
    }

    IEnumerator DestroyAttack(GameObject obj, float seconds)   // 공격 없애기
    {
        yield return new WaitForSeconds(seconds); // 지정된 시간 동안 대기
        Destroy(obj); // 오브젝트 파괴
    }
}