using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllUnit;
using System.Globalization;
using TMPro;

public class C_T001 : Cat, IAttack
{
    float skillEft = 5.0f;
    public float speed { get; set; } //���� �ӵ�
    public float atkTime { get; set; } //�Ϲݰ��� ��Ÿ��
    public float skillTime { get; set; } //��ų ���� ��Ÿ��
    public bool ativeSkill { get; set; }   //��ų Ȱ��ȭ �� ���� ����

    [SerializeField] GrowingData growingdata;


    //ĳ���� �� �ʱ�ȭ
    //DB���� �����
    //������ �Ҷ����� ���� ȣ�� + �� �ٽ� ��������
    private void InitData()
    {
        //ù ������ ������
        ID = "C_T001";
        Lv = 1;

        xhp = 3f;
        hpIncrease = 0.3f;
        maxHp = growingdata.Hp * xhp;
        hp = maxHp;

        xattack = 0.7f;
        attackIncrease = 0.05f;

        speed = 0f;    //����
        skillTime = 5f;
        atkTime = 2f;

        growingData = growingdata;
        Debug.Log(ID+"growingData ���� �Ϸ�");


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

    IEnumerator Skill()
    {
        this.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(skillEft);
    }

    private bool isAttack;
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

    //ĳ���� �̵� ���� ����
    private Rigidbody2D playerRb;
    //private Animator myAnim;
    private float playerMoveSpeed =1.0f;
    private Transform targetPosition;
    private Vector2 vel = Vector2.zero;
    

    private void FixedUpdate()
    {
        if (!isAttack) { Move(); }
    }
    private void Move()
    {
        //�� ĳ���� �������̸� ���̻� ������ x
        int layerMask = 1 << LayerMask.NameToLayer("Target");
        RaycastHit2D target = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 5f, layerMask);

       if(target)
        {
            //Ÿ���� ������ ��� Ÿ�������� �̵�
            float delta = Mathf.SmoothDamp(gameObject.transform.position.x, target.transform.position.x, ref vel.x, playerMoveSpeed);
            this.transform.position = new Vector2(delta, this.transform.position.y);
        }
        
        /*myAnim.SetFloat("MoveX", playerRb.velocity.x);
        myAnim.SetFloat("MoveY", playerRb.velocity.y);*/
    }
}
