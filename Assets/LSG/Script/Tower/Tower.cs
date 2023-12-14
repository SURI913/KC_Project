using DamageNumbersPro.Demo;
using DamageNumbersPro;
using UnityEngine;

public class Tower : MonoBehaviour, IDamageable, IAttack
{
    //기본 데이터

    public double hp { get; set; }      //체력
    public double maxHp { get; set; }   //최대체력
    protected double attack;  //공격력 전달할 때만 사용

    protected double healing = 0; //회복력
    protected double protection = 0; //방어력
    protected bool dead = false;    //죽음확인

    //타워 업그레이드에서 값 리셋해야함
    public int Lv { get; set; }
    public float LvEffect { get; set; }
    private float LvEffectIncreace = 0.01f;

    //IAttack
    public float speed { get; set; }   //공격 속도
    public float atkTime { get; set; } //일반공격 쿨타임
    public float skillTime { get; set; } = 0;
    public bool ativeSkill { get; set; } = false;
    protected GameObject damagePrefab;
    //-----------------------------------------------------------------------애니메이션
    private GameObject towerWheel;
    private float wheelSpeed = 15f;
    void initData()
    {
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

    [SerializeField] CurrentTowerData current_tower_data;
    private void Awake()
    {
        //레벨효과 = 1 + 0.01*레벨
        initData();
    }

    public double OnAttack(RaycastHit2D hit)
    {
        attack = current_tower_data.retention_attack * current_tower_data.attackX* LvEffect;
        
        return attack;
    }

    public double OnSkill(RaycastHit2D hit)
    {
        //타워는 스킬 x 나중에 파츠 고유 능력에서 사용하는걸로
        return 0;
    }   

    public void hpApply() //이후에 실시간으로 값 저장되면 수정하는 걸로
    {
        maxHp = current_tower_data.retention_hp * current_tower_data.hpX* LvEffect;
    }

    private double OnProtection()
    {
        protection = current_tower_data.retention_protection * current_tower_data.protectionX * LvEffect;
        return protection;
    }

    private void OnHealing() //회복 주기 타워입니동 마자요
    {
        healing = current_tower_data.retention_healing * current_tower_data.healingX * LvEffect;
        hp += healing;
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
        if (!dead) {
            DisplayDamageNumber(Damage);

            hp -= (Damage- OnProtection());
        }
        if(hp <= 0) { dead = true; // 씬의 처음으로 이동 //타워 죽음 처리
            hpInit();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
            OnHealing();
        }

        towerWheel.transform.Rotate(-Vector3.forward * Time.deltaTime * wheelSpeed);
        
    }
}
