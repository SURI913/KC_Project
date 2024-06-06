using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class BulletImpact : PoolAble
{
    private Vector3 startPos, endPos;
    //땅에 닫기까지 걸리는 시간
    protected float timer;
    protected float timeToFloor;

    public float my_speed;
    public ParticleSystem my_particle;

    public Transform init_transform { get; set; }
    LookTarget found_target_obj;

    private Transform target_none;
    Transform target
    {
        get
        {
            if (found_target_obj.colliders.Length > 0)
            {
                return found_target_obj.colliders[0].transform;
            }
            else return null;
        }

    }  //타겟 위치, 자식 위치로 가져와야함

    public AttackableImp my_attack_data { get; set; }
    public SkillUserImp my_skill_data { get; set; }

    bool is_loop = false; //true = attack, false = skill
    bool is_parabola = true;

    //캐싱 (가비지 생성 방지)
    public static readonly WaitForEndOfFrame wait_for_frame = new WaitForEndOfFrame(); // 캐싱

    public void MyHitData(AttackableImp my_data)
    {
        if (my_data != null)
        {
            is_loop = true;
            is_parabola = my_data.is_parabola_attack;
            my_attack_data = my_data;
            init_transform = my_data.my_attack_transform;
            transform.position = init_transform.position;
        }
    }
    public void MyHitData(SkillUserImp my_data)
    {
        if (my_data != null)
        {
            is_loop = false;
            is_parabola = my_data.is_parabola_skill;
            my_skill_data = my_data;
            init_transform = my_data.my_attack_transform;
            transform.position = init_transform.position;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //불값 걸어서 중복 방지
        if (collision.transform.gameObject.layer == 6 && !is_trigger) //"Target"레이어에 해당하는 오브젝트라면
        {
            is_trigger = true; //여기서 막아
            bool is_dead_collision = collision.GetComponent<PoolAble>();
            DamageableImp target = collision.GetComponent<DamageableImp>();
            if (target != null && !is_dead_collision)
            {
                if (is_loop && my_attack_data != null) {
                    //Debug.Log(my_attack_data);
                    target.OnDamage(my_attack_data.OnAttack(collision)); }
                else { target.OnDamage(my_skill_data.OnSkill(collision)); }
                Debug.Log(name + "공격 나갔습니다."+ collision.name +"이 맞았습니다.");
                ReleaseObject();
                return;
            }
            //이펙트 실행 후
        }
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        var mid = Vector3.Lerp(start, end, t);
        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    private Vector2 Straight(Vector3 end, float speed)
    {
        //Debug.Log(gameObject.name+" 작동유뮤체크");
        return Vector2.Lerp(transform.position, end, Time.deltaTime * speed);
    }
    private Quaternion Straight_Rotation(Vector3 end, float speed)
    {
        return Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(end-transform.position), Time.deltaTime * speed);
    }

    //포물선이냐 아니냐도 체크해야함
    private IEnumerator BulletMove()
    {
        Vector3 tempPos;
        while (true)
        {
            timer += Time.deltaTime;
            // Debug.Log(gameObject.name+is_parabola);
            if (is_parabola) { tempPos = Parabola(startPos, endPos, 5, timer); }
            else
            {
                tempPos = Straight(endPos, my_speed);
                if (my_particle)
                {
                    //Debug.Log("파티클 있음");

                    var direction = endPos - startPos;
                    float angle = Vector2.Angle(Vector2.left, direction);
                    ParticleSystem.MainModule main = my_particle.main;
                    main.startRotation = angle;
                }
                transform.rotation = Straight_Rotation(endPos, my_speed);
            }

            transform.position = tempPos;
            yield return wait_for_frame;
        }
    }
    Vector3 tempPos;
    private void Update()
    {
        timer += Time.deltaTime;
        if (is_parabola) { tempPos = Parabola(startPos, endPos, 5, timer); }
        else
        {
            tempPos = Straight(endPos, my_speed);
            if (my_particle)
            {
                //Debug.Log("파티클 있음");

                var direction = endPos - startPos;
                float angle = Vector2.Angle(Vector2.right, direction);
                ParticleSystem.MainModule main = my_particle.main;
                main.startRotation = angle;
            }
        }
        transform.position = tempPos;

        if (transform.position.y <-20)
        {
            ReleaseObject();
        }
    }

    private void Awake ()
    {
        found_target_obj = FindObjectOfType<LookTarget>(); //타겟 거리별로 감지하는 오브젝트 찾음 =>본인위치랑 비교함
        target_none = GameObject.FindWithTag("Plane").transform;
        if (found_target_obj == null) Debug.Log("found_target_obj 찾을 수 없음");
        my_particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        
        is_trigger = false;
        if (my_particle != null)
            my_particle.Play();
        ResetData();
    }
    private void Start()
    {
        ResetData();
        is_start = true;
        ReleaseObject();
    }
    bool is_start = false;
    void ResetData()
    {
        if (!is_start) return;
        //재 시작을 위한 세팅
        if (init_transform != null)
        {
            transform.position = init_transform.position;
            startPos = init_transform.position;

        }
        else
        {
            Debug.LogError("init_transform is null in BulletMove");
        }

        if (target == null) endPos = target_none.position + new Vector3(3, 0, 0);
        else endPos = target.position;
    }
}
