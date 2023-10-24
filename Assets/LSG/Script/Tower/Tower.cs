using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerUpHandler, IDamageable, IAttack
{
    //�⺻ ������

    public double hp { get; set; }      //ü��
    public double maxHp { get; set; }   //�ִ�ü��
    protected double attack;  //���ݷ� ������ ���� ���

    protected double healing = 0; //ȸ����
    protected bool dead = false;    //����Ȯ��
    public int Lv { get; set; }
    public float LvEffect { get; set; }
    private float LvEffectIncreace = 0.01f;

    //IAttack
    public float speed { get; set; }   //���� �ӵ�
    public float atkTime { get; set; } //�Ϲݰ��� ��Ÿ��
    public float skillTime { get; set; } = 0;
    public bool ativeSkill { get; set; } = false;

    void initData()
    {
        TowerUI.SetActive(false); //�⺻ ����
        Lv = 1;
        LvEffect = 1 + LvEffectIncreace * Lv;
        hpApply();
        hp = maxHp;

        //IAttack
        atkTime = 5f;
        speed = 15f;

    }

    [SerializeField] GameObject cannonData;
    [SerializeField] GameObject repairmanData;

    private TowerItem[] AllCannon;
    private TowerItem[] Allrepairman;
    private void Awake()
    {
        if (!cannonData) { Debug.Log("���� �����Ͱ� �����ϴ�."); }
        else
        {
            AllCannon = cannonData.GetComponentsInChildren<TowerItem>();
        }
        if (!repairmanData) { Debug.Log("t������ �����Ͱ� �����ϴ�."); }
        else
        {
            Allrepairman = repairmanData.GetComponentsInChildren<TowerItem>();
        }

        //������ ��������
        //����ȿ�� = 1 + 0.01*����
        initData();
    }

    public double OnAttack(RaycastHit2D hit)
    {
        attack = 0;
        foreach (var item in AllCannon)
        {
            if (item.Ative) //Ȱ��ȭ �� ���� ������
            {
                attack += item.RetentionEffect;
            }
            if (item.ChoiceItem) //������ �����۸� ������ ������ �������� ��� ���� ����
            {
                attack *= item.effect; //���ȿ��
            }
        }
        return attack;
    }

    public double OnSkill(RaycastHit2D hit)
    {
        return 0;
    }

    public void hpApply() //���Ŀ� �ǽð����� �� ����Ǹ� �����ϴ� �ɷ�
    {
        //������ �ݱ� ��ư �������� ����ǰԲ� ����
        foreach (var item in Allrepairman)
        {
            if (item.Ative) //Ȱ��ȭ �� ���� ������
            {
                maxHp += item.RetentionEffect;
            }
            if (item.ChoiceItem)
            {
                maxHp *= item.effect;
            }
        }
    }

    private void hpInit()
    {    //ü�� �ʱ�ȭ
        if (hp == -999 || maxHp == -999)
        {
            Debug.Log("���� hp error!");
        }
        else
        {
            hp = maxHp;
            dead = false;
        }
        //ü���� 0���� ���� ��� �ʱ�ȭ�� ���� �Ǿ���� �ڷ�ƾ �۾� �ʿ�
    }

    public void OnDamage(double Damage, RaycastHit2D hit)
    {
        if (!!dead) {
            hp-=Damage;
        }
        if(hp <= 0) { dead = true;}
    }

    //UI����
    public void OnPointerUp(PointerEventData data) // �޴����� ��� ����
    {
        Debug.Log("Ÿ�� Ȯ��");
        
    }
    [SerializeField] GameObject TowerUI;
    public void OnMouseUp()
    {
        Debug.Log("Ÿ�� Ȯ��");
        //Ȱ��ȭ
        TowerUI.SetActive(true);
    }


}
