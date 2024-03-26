using DamageNumbersPro.Demo;
using DamageNumbersPro;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;


public class Boss : PoolAble, DamageableImp
{
    // 보스 몬스터
    //private
    private float enemySpeed;              // 이동속도
    private double hp;                         // 보스 체력
    private double currentHp;             // 보스의 현재체력
    private float damage;                    // 보스공격의 데미지
    private bool isCollidedCastle = false; // 'Castle'과 충돌했는지 확인하는 변수
    private float originalEnemySpeed;     // 초기 enemySpeed 값을 저장하기 위한 변수
    private Coroutine attackCoroutine;  // Attack 코루틴을 저장하기 위한 변수
    private bool isAttack = true; // 코루틴을 시작할 때 공격을 허용하는 플래그
    private Enemy_Respown respawner;  // Enemy_Respown 스크립트의 참조
    private bool deadAnimation = false; // 캐릭터가 죽었는지 확인하고, update문에서 이동을 멈추는 역할
    private bool isDead = true;
    private Animator enemyAnimation; // Unity Animation 컴포넌트

    // public
    public float attackCooldown;  // 공격 쿨타임
    public float rayLength;           // 레이캐스트의 길이
    public GameObject boss_attack;  // 보스 공격 오브젝트
    public GameObject damagePrefab;

    void Start()
    {
        // 만약 enemyRespawner가 설정되지 않았다면, 현재 씬에서 Enemy_Respown 인스턴스를 찾아 설정
        // 이렇게 하면 에디터에서 수동으로 설정하지 않아도 자동으로 참조를 찾아줌
        if (!respawner)
        {
            respawner = FindObjectOfType<Enemy_Respown>();
        }

        transform.position = respawner.GetBossPosition();
        enemySpeed = respawner.GetEnemySpeed();
        originalEnemySpeed = enemySpeed;  // 처음 enemySpeed 값을 저장
        respawner = Enemy_Respown.Instance;  // 싱글톤 인스턴스를 통해 참조 설정
        hp = respawner.GetBossHp();

        enemyAnimation = GetComponent<Animator>();
    }

    public void SetStats(double health, float dmg)   // enemy_respown에서 설정한 체력, 데미지 불러오기
    {
        currentHp = health;   // 현재체력(이 스테이지의 보스체력)을 저장해서, 2페이즈 조건(체력 반틈이하 계산에서 사용)
        hp = health;
        damage = dmg;
    }

    public void OnDamage(double Damage)   //데미지를 입힘
    {
        hp -= Damage;
        DisplayDamageNumber(Damage);
        //Debug.Log("보스 " + gameObject.name + "이" + Damage + "만큼 데미지를 입었습니다.");
        if (hp <= 0)  // 보스가 죽으면
        {
            DeadAnimation();
            ReleaseObject();
            Debug.Log("보스" + gameObject.name + "처치");
            respawner.ShowStageClear();  // 보스가 죽었을 때 "Stage Clear!!" 표시
        }
    }

    IEnumerator DeadAnimation()
    {
        enemyAnimation.ResetTrigger("attack");
        enemyAnimation.SetTrigger("dead");
        yield return new WaitForSeconds(1.5f);
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

    IEnumerator BossFirstPage()   // 1페이즈
    {
        Debug.Log("1페이즈 시작");

        while (true) // 무한 반복
        {
            //enemyAnimation.SetTrigger("Enemy_attack");
            enemyAnimation.SetTrigger("attack");
            Vector3 spawnPosition = transform.position - Vector3.right * 5 + Vector3.up * 5;
            GameObject attackInstance = Instantiate(boss_attack, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 0.5f));

            yield return new WaitForSeconds(attackCooldown); // 쿨타임 동안 대기
        }
    }

    IEnumerator BossSecondPage()   // 2페이즈
    {
        Debug.Log("2페이즈 시작");

        while (true) // 무한 반복
        {   // 공격 3개생성
            //enemyAnimation.SetTrigger("Enemy_attack");
            enemyAnimation.SetTrigger("attack");
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

    void Update()
    {
        enemyAnimation.SetTrigger("walk");
        transform.Translate(Vector2.left * Time.deltaTime * enemySpeed);

        // Raycast를 사용하여 "Castle" 또는 "Player"를 감지
        Vector2 raycastStartPosition = new Vector2(transform.position.x, transform.position.y + 15);

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
                    if (hp > (currentHp / 2))
                    {
                        attackCoroutine = StartCoroutine(BossFirstPage());
                    }
                    else if (hp <= (currentHp / 2))
                    {
                        attackCoroutine = StartCoroutine(BossSecondPage());
                    }
                    isAttack = false; // 공격 코루틴을 한 번 시작하면 플래그를 false로 변경
                }
            }
        }
        else
        {
            if (deadAnimation)   // enemy가 죽었다면,
            {
                enemySpeed = 0; // 움직임 멈춤
            }
            else      //죽지않고, 충돌이 없어지면,
            {
                enemyAnimation.SetTrigger("walk");
                enemySpeed = originalEnemySpeed;  // 다시 이동
                isAttack = true;
            }
        }

        if (transform.position.x < -20)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator DestroyAttack(GameObject obj, float seconds)  // 공격 없애기
    {
        yield return new WaitForSeconds(seconds); // 지정된 시간 동안 대기
        Destroy(obj); // 오브젝트 파괴
    }
}
