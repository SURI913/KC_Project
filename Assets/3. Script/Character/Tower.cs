using DamageNumbersPro.Demo;
using DamageNumbersPro;
using UnityEngine;

public class Tower : BattleUnit
{
    //기본 데이터

    protected double attack;  //공격력 전달할 때만 사용
    public float atk_distance { get; set; } // 공격범위


    protected double healing = 0; //회복력
    protected double protection = 0; //방어력

    //타워 업그레이드에서 값 리셋해야함
    public int Lv { get; set; }
    public float LvEffect { get; set; }
    private float LvEffectIncreace = 0.01f;

    //IAttack
    public float speed { get; set; }   //공격 속도
    public float atk_time { get; set; } //일반공격 쿨타임
    //-----------------------------------------------------------------------애니메이션
    private GameObject towerWheel;
    private float wheelSpeed = 15f;
    public bool is_parabola_attack { get; set; }

    public Transform my_tool_pos;
    public Transform my_attack_transform { get { return my_tool_pos; } }

    void initData()
    {
        Lv = 1;
        LvEffect = 1 + LvEffectIncreace * Lv;

        //IAttack
        atk_time = 5f;
        speed = 15f;
        is_parabola_attack = true;
        atk_distance = 10; //원거리
        towerWheel = transform.GetChild(1).GetChild(0).gameObject;
    }

    [SerializeField] CurrentTowerData current_tower_data;
    private void Awake()
    {
        //레벨효과 = 1 + 0.01*레벨
        initData();
    }

    public double OnAttack(Collider2D collision)
    {
        attack = current_tower_data.retention_attack * current_tower_data.attackX* LvEffect;
        
        return attack;
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

    float hp_cooltime = 5f;
    private void Update()
    {
        //회복 쿨타임
        if (hp_cooltime > 0)
        {
            hp_cooltime -= Time.deltaTime;
        }
        else
        {
            hp_cooltime = 5f;
        }

        towerWheel.transform.Rotate(-Vector3.forward * Time.deltaTime * wheelSpeed);
        
    }

    
}
