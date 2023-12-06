using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_002 : MonoBehaviour, IDamageable
{
    // 근거리 몬스터 02
    public float enemySpeed;    // 몬스터 이동속도
    public Vector2 StartPosition;  // 몬스터 시작위치 
    public float attackCooldown;  // 공격 쿨타임
    private double hp;                  // 몬스터 체력
    private float damage;             // 몬스터의 데미지
    public GameObject enemy_attack_2;   // 공격시 소환할 공격개체
    private Transform target;                       // 타겟
    private float originalEnemySpeed;        // 공격이 끝난 후 다시 움직일때 할당할 이동값
    private Animator enemy_attack_animation;  // 공격 애니메이션
    private Coroutine attackCoroutine;               // 코루틴이 여러번 겹치지 않게할 변수
    private bool isAttack = true;
    public float rayLength;           // 레이캐스트의 길이

    void Start()
    {
        enemy_attack_animation = GetComponent<Animator>();
        transform.position = StartPosition;
        target = GameObject.FindGameObjectWithTag("Castle").transform;
        originalEnemySpeed = enemySpeed;
    }

    public void OnDamage(double Damage, RaycastHit2D hit)
    {
        hp -= Damage;
        Debug.Log("몬스터2가 " + Damage + "만큼 데미지를 입었습니다.");
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("몬스터2 처치");
        }
    }

    public void SetStats(double health, float dmg)
    {
        hp = health;
        damage = dmg;
    }

    void Update()
    {
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
            enemySpeed = originalEnemySpeed;
            isAttack = true;
        }

        if (transform.position.x < -20)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator Attack()
    {
        while (true) // 무한 반복
        {
            enemy_attack_animation.SetTrigger("Enemy_attack");
            Vector3 spawnPosition = transform.position - Vector3.right + (Vector3.up / 2);
            GameObject attackInstance = Instantiate(enemy_attack_2, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 1f));

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
