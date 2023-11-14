using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss : MonoBehaviour, IDamageable
{
    // ���� ����
    public float enemySpeed;              // �̵��ӵ�
    public Vector2 StartPosition;         // ��ȯ �� ��ġ
    public float attackCooldown = 2f;  // ���� ��Ÿ��
    private double hp;                         // ���� ü��
    private double currentHp;             // ������ ����ü��
    private float damage;                    // ���������� ������
    public GameObject boss_attack;  // ���� ���� ������Ʈ
    private bool bossSpawned = false;   // ����� ������ �Ͼ�°��� �����ϴ� ����
    private bool isCollidedCastle = false; // 'Castle'�� �浹�ߴ��� Ȯ���ϴ� ����
    private float originalEnemySpeed;     // �ʱ� enemySpeed ���� �����ϱ� ���� ����
    private Coroutine attackCoroutine;  // Attack �ڷ�ƾ�� �����ϱ� ���� ����
    private bool isAttack = true; // �ڷ�ƾ�� ������ �� ������ ����ϴ� �÷���

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
        Debug.Log("���� ���ݴ���");
        if (hp <= (currentHp / 2) && hp > 0 && !bossSpawned && isCollidedCastle)  
            // ü���� ��ƴ����, ������������, �� �ڵ尡 ���� ��������ʾҰ�, Castle�� �浹�ߴٸ� 2������ ����
        {
            bossSpawned = true; // �� ������ �߰��Ͽ� BossSecondPage()�� �� ���� ����ǵ��� ��
            StopCoroutine(BossFirstPage());    // 1������ ����
            Debug.Log("1������ ����, 2������ ����");
            StartCoroutine(BossSecondPage()); // 2������ ����
        }
        if (hp <= 0)  // ������ ������
        {
            Destroy(gameObject);
            Debug.Log("���� óġ");
            respawner.ShowStageClear();  // ������ �׾��� �� "Stage Clear!!" ǥ��
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle") || collision.collider.CompareTag("Player"))
        {
            Debug.Log("���� �浹");
            enemySpeed = 0;              // ������ ���߱�
            if (hp > (currentHp / 2))
            {
                Debug.Log("1������ ����");
                StartCoroutine(BossFirstPage()); // 1������ ����
            }
            isCollidedCastle = true; // Castle�� �浹������ ǥ��
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle") || collision.collider.CompareTag("Player"))
        {
            enemySpeed = originalEnemySpeed;  // �浹�� �������ٸ�, �ٽ� �̵�
        }
    }

    IEnumerator BossFirstPage()   // 1������
    {
        while (true) // ���� �ݺ�
        {
            boss_attack_animation.SetTrigger("M_boss_attack");  // isAttacking �Ķ���͸� true�� ����

            Vector3 spawnPosition = transform.position - Vector3.right*5+ Vector3.up*5;
            GameObject attackInstance = Instantiate(boss_attack, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 0.5f));

            yield return new WaitForSeconds(attackCooldown); // ��Ÿ�� ���� ���
        }
    }

    IEnumerator BossSecondPage()   // 2������
    {
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

    IEnumerator DestroyAttack(GameObject obj, float seconds)  // ���� ���ֱ�
    {
        yield return new WaitForSeconds(seconds); // ������ �ð� ���� ���
        Destroy(obj); // ������Ʈ �ı�
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * enemySpeed);

        if (transform.position.x < -20)            // x������ -20���� ���� (ȭ�� ��)
        {
            gameObject.SetActive(false);
        }
    }
}
