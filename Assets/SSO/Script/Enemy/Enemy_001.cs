using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_001 : MonoBehaviour
{
    // �ٰŸ� ���� 01
    public float enemySpeed;    // ���� �̵��ӵ�
    public Vector2 StartPosition;  // ���� ������ġ 
    public float attackCooldown;  // ���� ��Ÿ��
    private double hp;                  // ���� ü��
    private float damage;             // ������ ������
    public GameObject enemy_attack_1;   // ���ݽ� ��ȯ�� ���ݰ�ü
    private Transform target;                       // Ÿ��
    private float originalEnemySpeed;        // ������ ���� �� �ٽ� �����϶� �Ҵ��� �̵���
    private Animator enemy_attack_animation;  // ���� �ִϸ��̼�
    private Coroutine attackCoroutine;               // �ڷ�ƾ�� ������ ��ġ�� �ʰ��� ����
    private bool isAttack = true;
    public float rayLength;           // ����ĳ��Ʈ�� ����

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
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("����1 óġ");
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

        // Raycast�� ����Ͽ� "Castle" �Ǵ� "Player"�� ����
        Vector2 raycastStartPosition = new Vector2(transform.position.x, transform.position.y + 1);
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
                    attackCoroutine = StartCoroutine(Attack());
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

    IEnumerator Attack()
    {
        while (true) // ���� �ݺ�
        {
            enemy_attack_animation.SetTrigger("Enemy_attack");
            Vector3 spawnPosition = transform.position - Vector3.right + (Vector3.up / 2);
            GameObject attackInstance = Instantiate(enemy_attack_1, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 1f));

            // ���
            yield return new WaitForSeconds(attackCooldown);

            // �߻�ü ������ ������ ����
            Destroy(attackInstance);
        }
    }

    IEnumerator DestroyAttack(GameObject obj, float seconds)   // ���� ���ֱ�
    {
        yield return new WaitForSeconds(seconds); // ������ �ð� ���� ���
        Destroy(obj); // ������Ʈ �ı�
    }
}
