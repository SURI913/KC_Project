using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    // 보스 몬스터
    public float enemySpeed = 0;
    public Vector2 StartPosition;
    public GameObject boss_attack;
    public float attackCooldown = 2f;  // 공격 쿨타임
    public float hp = 1000.0f;
    public float damage = 10.0f;
    private bool bossSpawned = false;   // 페이즈가 여러번 일어나는것을 방지하는 변수
    private bool isCollidedCastle = false; // 'Castle'과 충돌했는지 확인하는 변수


    void Start()
    {
        transform.position = StartPosition;
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //데미지를 입힘
    {
        hp -= damage;
        Debug.Log("보스 공격당함");
        if (hp <= 500 && hp > 0 && !bossSpawned && isCollidedCastle)  
            // 체력이 반틈이하, 죽지않은상태, 이 코드가 아직 실행되지않았다면, Castle과 충돌했다면 2페이즈 시작
        {
            bossSpawned = true; // 이 변수를 추가하여 BossSecondPage()가 한 번만 실행되도록 함
            StopCoroutine(BossFirstPage());
            Debug.Log("1페이즈 종료, 2페이즈 시작");
            StartCoroutine(BossSecondPage()); // Coroutine 시작
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("보스 처치");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle"))
        {
            Debug.Log("보스 충돌");
            enemySpeed = 0;
            if (hp > 500)
            {
                Debug.Log("1페이즈 시작");
                StartCoroutine(BossFirstPage()); // Coroutine 시작
            }
            isCollidedCastle = true; // Castle과 충돌했음을 표시
        }
    }

    IEnumerator BossFirstPage()   // 1페이즈
    {
        while (true) // 무한 반복
        {
            Vector3 spawnPosition = transform.position - Vector3.right*3;
            GameObject attackInstance = Instantiate(boss_attack, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 0.5f));

            yield return new WaitForSeconds(attackCooldown); // 쿨타임 동안 대기
        }
    }

    IEnumerator BossSecondPage()   // 2페이즈
    {
        while (true) // 무한 반복
        {   // 공격 3개생성
            Vector3 spawnPosition = transform.position - Vector3.right * 3;                   // 중간공격
            Vector3 spawnPosition2 = spawnPosition - Vector3.right - Vector3.up;       // 위
            Vector3 spawnPosition3 = spawnPosition - Vector3.right - Vector3.down;  // 아래
            GameObject attackInstance = Instantiate(boss_attack, spawnPosition, Quaternion.identity);
            GameObject attackInstance2 = Instantiate(boss_attack, spawnPosition2, Quaternion.identity);
            GameObject attackInstance3 = Instantiate(boss_attack, spawnPosition3, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 0.5f));
            StartCoroutine(DestroyAttack(attackInstance2, 0.5f));
            StartCoroutine(DestroyAttack(attackInstance3, 0.5f));

            yield return new WaitForSeconds(attackCooldown); // 쿨타임 동안 대기
        }
    }

    IEnumerator DestroyAttack(GameObject obj, float seconds)  // 공격 없애기
    {
        yield return new WaitForSeconds(seconds); // 지정된 시간 동안 대기
        Destroy(obj); // 오브젝트 파괴
    }


    void Update()
    {

        transform.Translate(Vector2.right * Time.deltaTime * enemySpeed);

        if (transform.position.x < -15)
        {
            gameObject.SetActive(false);
        }

    }
}
