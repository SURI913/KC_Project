using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss : MonoBehaviour, IDamageable
{
    // 보스 몬스터
    public float enemySpeed;              // 이동속도
    public Vector2 StartPosition;         // 소환 될 위치
    public float attackCooldown = 2f;  // 공격 쿨타임
    private double hp;                         // 보스 체력
    private double currentHp;             // 보스의 현재체력
    private float damage;                    // 보스공격의 데미지
    public GameObject boss_attack;  // 보스 공격 오브젝트
    private bool bossSpawned = false;   // 페이즈가 여러번 일어나는것을 방지하는 변수
    private bool isCollidedCastle = false; // 'Castle'과 충돌했는지 확인하는 변수
    private float originalEnemySpeed;     // 초기 enemySpeed 값을 저장하기 위한 변수
    private Coroutine attackCoroutine;  // Attack 코루틴을 저장하기 위한 변수
    private bool isAttack = true; // 코루틴을 시작할 때 공격을 허용하는 플래그

    private Enemy_Respown respawner;  // Enemy_Respown 스크립트의 참조
    private Animator boss_attack_animation;  // 보스의 공격 애니메이션


    void Start()
    {
        boss_attack_animation = GetComponent<Animator>();

        originalEnemySpeed = enemySpeed;  // 처음 enemySpeed 값을 저장
        transform.position = StartPosition;
        respawner = Enemy_Respown.Instance;  // 싱글톤 인스턴스를 통해 참조 설정
    }

    public void SetStats(double health, float dmg)   // enemy_respown에서 설정한 체력, 데미지 불러오기
    {
        currentHp = health;   // 현재체력(이 스테이지의 보스체력)을 저장해서, 2페이즈 조건(체력 반틈이하 계산에서 사용)
        hp = health;
        damage = dmg;
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //데미지를 입힘
    {
        hp -= Damage;
        Debug.Log("보스 공격당함");
        if (hp <= (currentHp / 2) && hp > 0 && !bossSpawned && isCollidedCastle)  
            // 체력이 반틈이하, 죽지않은상태, 이 코드가 아직 실행되지않았고, Castle과 충돌했다면 2페이즈 시작
        {
            bossSpawned = true; // 이 변수를 추가하여 BossSecondPage()가 한 번만 실행되도록 함
            StopCoroutine(BossFirstPage());    // 1페이즈 종료
            Debug.Log("1페이즈 종료, 2페이즈 시작");
            StartCoroutine(BossSecondPage()); // 2페이즈 시작
        }
        if (hp <= 0)  // 보스가 죽으면
        {
            Destroy(gameObject);
            Debug.Log("보스 처치");
            respawner.ShowStageClear();  // 보스가 죽었을 때 "Stage Clear!!" 표시
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle") || collision.collider.CompareTag("Player"))
        {
            Debug.Log("보스 충돌");
            enemySpeed = 0;              // 움직임 멈추기
            if (hp > (currentHp / 2))
            {
                Debug.Log("1페이즈 시작");
                StartCoroutine(BossFirstPage()); // 1페이즈 시작
            }
            isCollidedCastle = true; // Castle과 충돌했음을 표시
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle") || collision.collider.CompareTag("Player"))
        {
            enemySpeed = originalEnemySpeed;  // 충돌이 없어졌다면, 다시 이동
        }
    }

    IEnumerator BossFirstPage()   // 1페이즈
    {
        while (true) // 무한 반복
        {
            boss_attack_animation.SetTrigger("M_boss_attack");  // isAttacking 파라미터를 true로 설정

            Vector3 spawnPosition = transform.position - Vector3.right*5+ Vector3.up*5;
            GameObject attackInstance = Instantiate(boss_attack, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 0.5f));

            yield return new WaitForSeconds(attackCooldown); // 쿨타임 동안 대기
        }
    }

    IEnumerator BossSecondPage()   // 2페이즈
    {
        while (true) // 무한 반복
        {   // 공격 3개생성
            boss_attack_animation.SetTrigger("M_boss_attack");  // isAttacking 파라미터를 true로 설정
            Vector3 spawnPosition = transform.position - Vector3.right * 5 + Vector3.up * 5;    // 중간공격
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
        transform.Translate(Vector2.left * Time.deltaTime * enemySpeed);

        if (transform.position.x < -20)            // x축으로 -20까지 가면 (화면 밖)
        {
            gameObject.SetActive(false);
        }
    }
}
