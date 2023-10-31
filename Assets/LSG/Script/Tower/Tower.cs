using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerUpHandler, IDamageable, IAttack
{
    //기본 데이터

    public double hp { get; set; }      //체력
    public double maxHp { get; set; }   //최대체력
    protected double attack;  //공격력 전달할 때만 사용

    protected double healing = 0; //회복력
    protected bool dead = false;    //죽음확인
    public int Lv { get; set; }
    public float LvEffect { get; set; }
    private float LvEffectIncreace = 0.01f;

    //IAttack
    public float speed { get; set; }   //공격 속도
    public float atkTime { get; set; } //일반공격 쿨타임
    public float skillTime { get; set; } = 0;
    public bool ativeSkill { get; set; } = false;

    //-----------------------------------------------------------------------애니메이션
    private GameObject towerWheel;
    private float wheelSpeed = 15f;
    void initData()
    {
        TowerUI.SetActive(false); //기본 설정
        Lv = 1;
        LvEffect = 1 + LvEffectIncreace * Lv;
        hpApply();
        hp = maxHp;

        //IAttack
        atkTime = 5f;
        speed = 15f;

        towerWheel = transform.GetChild(1).GetChild(0).gameObject;
        Debug.Log(towerWheel.name);
    }

    [SerializeField] GameObject cannonData;
    [SerializeField] GameObject repairmanData;

    private TowerItem[] AllCannon;
    private TowerItem[] Allrepairman;
    private void Awake()
    {
        if (!cannonData) { Debug.Log("대포 데이터가 없습니다."); }
        else
        {
            AllCannon = cannonData.GetComponentsInChildren<TowerItem>();
        }
        if (!repairmanData) { Debug.Log("t수리공 데이터가 없습니다."); }
        else
        {
            Allrepairman = repairmanData.GetComponentsInChildren<TowerItem>();
        }

        //데이터 가져오기
        //레벨효과 = 1 + 0.01*레벨
        initData();
    }

    public double OnAttack(RaycastHit2D hit)
    {
        attack = 0;
        foreach (var item in AllCannon)
        {
            if (item.Ative) //활성화 된 값만 가져옴
            {
                attack += item.RetentionEffect;
            }
            if (item.ChoiceItem) //선택한 아이템만 가져옴 아이템 가져오는 방식 이후 수정
            {
                attack *= item.effect; //배수효과
            }
        }
        return attack;
    }

    public double OnSkill(RaycastHit2D hit)
    {
        return 0;
    }   

    public void hpApply() //이후에 실시간으로 값 저장되면 수정하는 걸로
    {
        
        //지금은 닫기 버튼 눌렀을때 저장되게끔 설정
        foreach (var item in Allrepairman)
        {
            if (item.Ative) //활성화 된 값만 가져옴
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
    {    //체력 초기화
        if (hp == -999 || maxHp == -999)
        {
            Debug.Log("대포 hp error!");
        }
        else
        {
            hp = maxHp;
            dead = false;
        }
        //체력이 0보다 작을 경우 초기화가 실행 되어야함 코루틴 작업 필요
    }

    public void OnDamage(double Damage, RaycastHit2D hit)
    {
        if (!!dead) {
            hp-=Damage;
        }
        if(hp <= 0) { dead = true;}
    }

    //UI띄우기
    public void OnPointerUp(PointerEventData data) // 휴대폰의 경우 수정
    {
        Debug.Log("타워 확인");
        
    }

    [SerializeField] GameObject TowerUI;
    public void OnMouseUp()
    {
        Debug.Log("타워 확인");
        //활성화
        TowerUI.SetActive(true);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) //=> Target Layer
        {
            //적  에게 막혔음
            wheelSpeed = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6) //=> Target Layer
        {
            //적 사라짐 바퀴 움직일 것
            wheelSpeed = 15f;
        }
    }

    private void Update()
    {
        
        towerWheel.transform.Rotate(-Vector3.forward * Time.deltaTime * wheelSpeed);
        
    }
}
