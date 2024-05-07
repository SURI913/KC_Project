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
    bool is_parabola = false;
    public void MyHitData(AttackableImp my_data)
    {
        if (my_data != null)
        {
            is_loop = true;
            is_parabola = my_data.is_parabola_attack;
            my_attack_data = my_data;
        }
    }
    public void MyHitData(SkillUserImp my_data)
    {
        if (my_data != null)
        {
            is_loop = false;
            is_parabola = my_data.is_parabola_skill;
            my_skill_data = my_data;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (is_trigger) return; // 이미 처리 중인 충돌이 있으면 반환
        //불값 걸어서 중복 방지
        if (collision.transform.gameObject.layer == 6 && !is_trigger) //"Target"레이어에 해당하는 오브젝트라면
        {
            is_trigger = true; //여기서 막아
            DamageableImp target = collision.GetComponent<DamageableImp>();
            if (target != null)
            {
                if (is_loop && my_attack_data != null) {
                    Debug.Log(my_attack_data);
                    target.OnDamage(my_attack_data.OnAttack(collision)); }
                else { target.OnDamage(my_skill_data.OnSkill(collision)); }
                //Debug.Log(name + "공격 나갔습니다."+ collision.name +"이 맞았습니다.");
                ReleaseObject();
                return;
            }
            //이펙트 실행 후
        }
        else if (collision.tag == "Plane" && !is_trigger)
        {
            is_trigger = true;
            //Debug.Log(name + "공격 나갔습니다. 바닥이 맞았습니다.");
            ReleaseObject();           
        }
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    private Vector2 Straight(Vector2 start, Vector2 end, float speed)
    {
        return Vector2.Lerp(start, end, Time.deltaTime);

    }

    //포물선이냐 아니냐도 체크해야함
    private IEnumerator BulletMove()
    {
        timer = 0;
        Vector3 tempPos;
        while (transform.position.y >= startPos.y)
        {
            timer += Time.deltaTime;
            
            if (is_parabola) { tempPos = Parabola(startPos, endPos, 5, timer); }
            else { tempPos = Straight(startPos, endPos, my_speed); }
            
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }
    }

    private void Awake ()
    {
        
        found_target_obj = FindObjectOfType<LookTarget>(); //타겟 거리별로 감지하는 오브젝트 찾음 =>본인위치랑 비교함
        target_none = GameObject.FindWithTag("Plane").transform;
        if (found_target_obj == null) Debug.Log("found_target_obj 찾을 수 없음");
        
    }

    private void OnEnable()
    {
        if (ObjectPoolManager.instance.IsReady)
        {
            startPos = transform.position;
            if (target != null) { endPos = target.position; }
            else { endPos = target_none.position + new Vector3(10, 0, 0); }
            StartCoroutine("BulletMove");
            is_trigger = false;
        }
    }

    private void OnDisable()
    {
        if (ObjectPoolManager.instance.IsReady && !init_transform)
        {
            transform.position = init_transform.position;
            StopCoroutine("BulletMove");

        }
    }

}
