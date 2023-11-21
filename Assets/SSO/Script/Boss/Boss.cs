using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss : MonoBehaviour, IDamageable
{
    // ���� ����
    public float enemySpeed;              // �̵��ӵ�
    public Vector2 StartPosition;         // ��ȯ �� ��ġ
    public float attackCooldown;  // ���� ��Ÿ��
    private double hp;                         // ���� ü��
    private double currentHp;             // ������ ����ü��
    private float damage;                    // ���������� ������
    public GameObject boss_attack;  // ���� ���� ������Ʈ
    private bool isCollidedCastle = false; // 'Castle'�� �浹�ߴ��� Ȯ���ϴ� ����
    private float originalEnemySpeed;     // �ʱ� enemySpeed ���� �����ϱ� ���� ����
    private Coroutine attackCoroutine;  // Attack �ڷ�ƾ�� �����ϱ� ���� ����
    private bool isAttack = true; // �ڷ�ƾ�� ������ �� ������ ����ϴ� �÷���
    public float rayLength;           // ����ĳ��Ʈ�� ����

    private Enemy_Respown respawner;  // Enemy_Respown ��ũ��Ʈ�� ����
    private Animator boss_attack_animation;  // ������ ���� �ִϸ��̼�

    void Start()
    {
        boss_attack_animation = GetComponent<Animator>();

        originalEnemySpeed = enemySpeed;  // ó�� enemySpeed ���� ����
        transform.position = StartPosition;
        respawner = Enemy_Respown.Instance;  // �̱��� �ν��Ͻ��� ���� ���� ����
    }

    public void SetStats(double health, float dmg)   // enemy_respown���� ������ ü��, ������ �ҷ�����
    {
        currentHp = health;   // ����ü��(�� ���������� ����ü��)�� �����ؼ�, 2������ ����(ü�� ��ƴ���� ��꿡�� ���)
        hp = health;
        damage = dmg;
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //�������� ����
    {
        hp -= Damage;
        Debug.Log("������ ���ݴ���");
        if (hp <= 0)  // ������ ������
        {
            Destroy(gameObject);
            Debug.Log("���� óġ");
            respawner.ShowStageClear();  // ������ �׾��� �� "Stage Clear!!" ǥ��
        }
    }

    IEnumerator BossFirstPage()   // 1������
    {
        Debug.Log("1������ ����");

        while (true) // ���� �ݺ�
        {
            boss_attack_animation.SetTrigger("M_boss_attack");  // isAttacking �Ķ���͸� true�� ����

            Vector3 spawnPosition = transform.position - Vector3.right * 5 + Vector3.up * 5;
            GameObject attackInstance = Instantiate(boss_attack, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 0.5f));

            yield return new WaitForSeconds(attackCooldown); // ��Ÿ�� ���� ���
        }
    }

    IEnumerator BossSecondPage()   // 2������
    {
        Debug.Log("2������ ����");

        while (true) // ���� �ݺ�
        {   // ���� 3������
            boss_attack_animation.SetTrigger("M_boss_attack");  // isAttacking �Ķ���͸� true�� ����
            Vector3 spawnPosition = transform.position - Vector3.right * 5 + Vector3.up * 5;    // �߰�����
            Vector3 spawnPosition2 = spawnPosition - Vector3.right - Vector3.up;       // ��
            Vector3 spawnPosition3 = spawnPosition - Vector3.right - Vector3.down;  // �Ʒ�
            GameObject attackInstance = Instantiate(boss_attack, spawnPosition, Quaternion.identity);
            GameObject attackInstance2 = Instantiate(boss_attack, spawnPosition2, Quaternion.identity);
            GameObject attackInstance3 = Instantiate(boss_attack, spawnPosition3, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 0.5f));
            StartCoroutine(DestroyAttack(attackInstance2, 0.5f));
            StartCoroutine(DestroyAttack(attackInstance3, 0.5f));

            yield return new WaitForSeconds(attackCooldown); // ��Ÿ�� ���� ���
        }
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * enemySpeed);

        // Raycast�� ����Ͽ� "Castle" �Ǵ� "Player"�� ����
        Vector2 raycastStartPosition = new Vector2(transform.position.x, transform.position.y + 5);
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPosition, Vector2.left, rayLength, LayerMask.GetMask("Castle", "Player"));

        // Ray�� �ð������� ǥ��
        Debug.DrawRay(raycastStartPosition, Vector2.left * rayLength, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Castle") || hit.collider.CompareTag("Player"))
            {
                enemySpeed = 0;

                // ���� �÷��װ� true�� ��쿡�� ���� �ڷ�ƾ�� ����
                if (isAttack)
                {
                    // ������ ���� ���̴� Attack �ڷ�ƾ�� ����
                    if (attackCoroutine != null)
                    {
                        StopCoroutine(attackCoroutine);
                    }

                    // Attack �ڷ�ƾ�� ����
                    if (hp > (currentHp / 2))
                    {
                        attackCoroutine = StartCoroutine(BossFirstPage());
                    }
                    else if (hp <= (currentHp / 2))
                    {
                        attackCoroutine = StartCoroutine(BossSecondPage());
                    }
                    isAttack = false; // ���� �ڷ�ƾ�� �� �� �����ϸ� �÷��׸� false�� ����
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

    IEnumerator DestroyAttack(GameObject obj, float seconds)  // ���� ���ֱ�
    {
        yield return new WaitForSeconds(seconds); // ������ �ð� ���� ���
        Destroy(obj); // ������Ʈ �ı�
    }
}
