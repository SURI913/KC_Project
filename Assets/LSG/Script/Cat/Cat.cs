using System.Collections;
using UnityEngine;
using AllUnit;
using DamageNumbersPro.Demo;
using DamageNumbersPro;
using System;

public class Cat : MonoBehaviour, DamageableImp, SkillUserImp, AttackableImp
{
    protected int lv = 0;
    public double hp { get; set; }
    protected float boss_attack = 0;  //보스 데미지 피해량 
    bool is_passive_skill = false;
    protected float skill_effect;
    public bool dead { get; set; } = false;    //죽음확인

    public GameObject damage_prefab;
    //------------------------------------------------------초기값 저장
    public BaseCatData cat_data { get; set; }
    public GrowingData growing_data { get; set; }
    //장비 멀로 처리하냐

    //-----------------------------------------------------------------------------------------------------------애니메이션
    protected Animator cat_motion;    

    public float respawnTime = 8f;

    [SerializeField]
    private SkillData my_skill_data;
    //---------------------------------------------------------------SkillUserImp
    public float skill_distance { get; set; }

    public float speed { get; set; }   //공격 속도
    public float skill_time { get; set; }   //스킬 공격 쿨타임
    public bool is_ative_skill { get; set; } = false;   //스킬 활성화 시 공격 멈춤
    //--------------------------------------------------------------AttackableImp
    public float atk_time
    {
        get { return cat_data._atk_time; }
        set { atk_distance = value; }
    } //일반공격 쿨타임 값 초기화 가 안된다면 이렇게 구현
    public float atk_distance { get; set; } // 공격범위
    public bool is_parabola_skill { get; set; }
    public bool is_parabola_attack { get; set; }

    protected void initAttackData()
    {
        speed = cat_data._attack_speed;
        atk_time = cat_data._atk_time;
        skill_time = cat_data._skl_time;
        skill_effect = cat_data._skl_effect;
    }

    protected void hpInit(){    //체력 초기화
        if(!growing_data)
        {
            Debug.Log(cat_data._id);
            Debug.Log("hp error!");
        }
       else
       {
            hp = growing_data.Hp * cat_data._hp_multipler;
            dead = false;
            cat_motion.SetBool("isDead", false);
       }
            //체력이 0보다 작을 경우 초기화가 실행 되어야함
    }

    //캐릭터 데미지를 입히고 싶을 때 레이캐스트 판정(tag) Cat걸리면 실행시키면 됨 당사자의 데미지를 까고 0이면 hit 캐릭터 삭제?
    public void OnDamage(double Damage) //캐릭터 데미지 입히는 호출
    {
        if (!dead)
        {
            //데미지 입음
            DisplayDamageNumber(Damage);
            double damageAdd = Damage;//-(영웅 방어력*방어력(보유효과)*성급효과(뭔지모르겠음)*패시브스킬(유무))
            hp -= damageAdd;
            if (hp <= 0 && !dead) { Die(); }    //죽음처리
        }
        //패시브 스킬이 있는경우 오버라이딩으로 작업
    }

    //데미지 프리펩
    void DisplayDamageNumber(double Damage)
    {
        DamageNumber prefab;
        prefab = damage_prefab.GetComponent<DamageNumber>();


        DNP_PrefabSettings settings = DNP_DemoManager.instance.GetSettings();

        // 생성된 데미지 숫자에 데미지 및 설정을 적용
        DamageNumber newDamageNumber = prefab.Spawn(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), (float)Damage);
        newDamageNumber.SetFollowedTarget(transform);

        // 설정 적용
        settings.Apply(newDamageNumber);
    }

    public virtual double OnSkill(Collider2D collision)
    {
        return -999;
    }

    public virtual double OnAttack(Collider2D collision) //공격값 계산
    {
        return -999;
    }

    public virtual void OnHealing(double value){
        //체력 회복
        if(cat_data._healing_multiple == 0){
            Debug.Log("ID");
            Debug.Log("healing error!");
            return;
        }
        hp += value;
        var effect_obj = ObjectPoolManager.instance.GetGo("Healing_Effect");
        effect_obj.transform.position = this.transform.position;
        effect_obj.GetComponent<MeleeImpact>().init_transform = transform;

        //영웅 회복력*회복력(보유효과)*장비장착효과(유무)*성급효과*별자리(유무)
    }

    public double GetAttackPower(){
        //일반 공격값 반환
        if (cat_data._attack_multipler == 0){
            Debug.Log(cat_data._id);
            Debug.Log("attack error!");
            return 0;
        }
        if(growing_data == null)
        {
            Debug.Log("공격 중 growingDataError!");

        }
        cat_motion.SetTrigger("isAttack");

        double AllAttack = growing_data.Attack*cat_data._attack_multipler;
        //Debug.Log(ID+(int)AllAttack);
        //영웅 공격력*공격력(보유효과)*성급효과*장비장착효과*패시브스킬*별자리
        //패시브 스킬은 어떻게 짤건지 고민 + 크리티컬 데미지 작업도 필요함
        //-----------------------------------------------------------------------------------------------------------애니메이션 추가
        return AllAttack;
    }

    //리스폰타임 뒤 체력 회복
    //애니메이션 처리도 여기서
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        cat_motion.SetTrigger("FinishStune");
        hpInit();
    }

    private void Die()
    {
        dead = true;
        //캐릭터 죽는 모션
        cat_motion.SetBool("isDead", true);
        StartCoroutine(Respawn());
    }

    public void LevelUP()
    {
        if(ativelevelup)
        {
            Debug.Log(cat_data._id + "레벨업!");
            if (growing_data == null)
            {
                Debug.Log("레벨업 중 growingData Error!");

            }
            lv++;
            cat_data._attack_multipler += cat_data._increase_attack;
            cat_data._hp_multipler += cat_data._increase_hp;
            ativelevelup = false;
        }
    }

    protected void printData()
    {
        Debug.Log(cat_data._id + " hp: "+Unit.ToUnitString(hp));
        Debug.Log(cat_data._id + "maxHp: " +Unit.ToUnitString(growing_data.Hp * cat_data._hp_multipler));
        Debug.Log(cat_data._id + "attack: " +Unit.ToUnitString(growing_data.Attack * cat_data._attack_multipler));
        Debug.Log(cat_data._id + "Lv: " +lv);
    }

    //일시적으로 제한
    private float cooltime = 10f;
    private bool ativelevelup = true;

    /*private void Update()
    {
        //레벨업 제한하는 부분 수정해야함
        if (cooltime >= 0 && !ativelevelup)
        {
            cooltime -= Time.deltaTime;

        }
        else
        {
            ativelevelup = true;
            cooltime = 10f;
        }

    }*/

    //캐릭터 움직임을 위한 변수
    protected Rigidbody2D player_rb;
    private float playerMoveSpeed = 7f;
    private Vector2 vel = Vector2.zero;
    protected bool is_attack = false;

    public float size;
    public LayerMask layer_mask;

    protected RaycastHit2D target;
    protected bool isLookTarget = false;

    public void Move()
    {
        //레이캐스트로 타겟 체크 후 움직임
        if (!isLookTarget && is_attack)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, size, layer_mask);
            Array.Sort(colliders, new DistanceComparer(transform));
            //타겟 방향으로 이동을 시키나
            target = Physics2D.BoxCast(gameObject.transform.position, transform.lossyScale * 20, 0f, Vector2.right, 0f, LayerMask.GetMask("Target"));
            //target = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 1f, layerMask);
            if (target && !target.collider.GetComponent<Enemy_004>())//위 원거리 딜러
            {
                isLookTarget = true;
            }
            else
            {
                isLookTarget = false;

            }

        }
        if (target)
        {
            //물리로 움직이는 방향 변경
            float delta = Mathf.SmoothDamp(gameObject.transform.position.x, target.transform.position.x, ref vel.x, playerMoveSpeed);
            transform.position = new Vector2(delta, transform.position.y);
        }
        /*myAnim.SetFloat("MoveX", playerRb.velocity.x); //나중에 맞춰서 수정
        myAnim.SetFloat("MoveY", playerRb.velocity.y);*/
    }
}