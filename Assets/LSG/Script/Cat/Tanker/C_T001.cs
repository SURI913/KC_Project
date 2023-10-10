using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;
using System.Globalization;
using TMPro;

public class C_T001 : Cat, IAttack
{
    float skillEft = 5.0f;
    public float speed { get; set; } //���� �ӵ�
    public float atkTime { get; set; } //�Ϲݰ��� ��Ÿ��
    public float skillTime { get; set; } //��ų ���� ��Ÿ��
    public bool AtiveSkill { get; set; }   //��ų Ȱ��ȭ �� ���� ����

    //ĳ���� �� �ʱ�ȭ
    //DB���� �����
    //������ �Ҷ����� ���� ȣ�� + �� �ٽ� ��������
    private void InitData()
    {
        //ù ������ ������
        ID = "C_T001";
        maxHp = 3000;
        hp = maxHp;
        attack = 10;
        Lv = 1;
        speed = 15f;    //����
        skillTime = 5f;
        atkTime = 2f;
    }

    private void Awake()
    {
        //�����Ͱ� ������
        InitData();
        //�����Ͱ� ������

        //�̵� �� ���� ����
        playerRb = GetComponent<Rigidbody2D>();
        //myAnim = GetComponent<Animator>();
    }

    public double OnSkill(RaycastHit2D hit)
    {
        //5�ʵ��� �޴� ���ط� 0
        StartCoroutine(Skill());
        return 0;
    }

    public double OnAttack(RaycastHit2D hit) //���� üũ
    {
        if (hit.collider.CompareTag("Respawn")) //���� ������ ���
        {
            return attackApply() + bossAttack;
        }
        return attackApply();
    }

    public void levelUP()
    {
        LevelUP();
    }

    IEnumerator Skill()
    {
        this.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(skillEft);
    }

    public bool isAttack { get; set; }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isAttack)
        {
            isAttack = true;
            //�ٽ� �۾� �ʿ� ��Ŀ�� ��ũ��Ʈ ���� �ʿ�
            IDamageable damageable = collision.collider.GetComponent<IDamageable>();
            //���� �ε�ġ�� ���� ���� �Ǹ� ����
            if (damageable != null)
            {
                RaycastHit2D EnemyRayHit = collision.collider.GetComponent<RaycastHit2D>();
                collision.collider.GetComponent<IDamageable>().OnDamage(OnAttack(EnemyRayHit), EnemyRayHit);
            }
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isAttack = false;
    }

    private Rigidbody2D playerRb;
    //private Animator myAnim;
    public float playerMoveSpeed =1.0f;
    public Transform targetPosition;
    Vector2 vel = Vector2.zero;
    

    private void FixedUpdate()
    {
        if (!isAttack) { Move(); }
    }
    void Move()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Target");
        RaycastHit2D target = Physics2D.Raycast(gameObject.transform.position, Vector2.right, layerMask);

       if(target)
        {
            //Ÿ���� ������ ��� Ÿ�������� �̵�
            float delta = Mathf.SmoothDamp(gameObject.transform.position.x, target.transform.position.x, ref vel.x, 3f);
            this.transform.position = new Vector2(delta, this.transform.position.y);
        }
        
        /*myAnim.SetFloat("MoveX", playerRb.velocity.x);
        myAnim.SetFloat("MoveY", playerRb.velocity.y);*/
    }
}
