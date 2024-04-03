using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : PoolAble
{
    [Header("* 움직임 선택")]
    public int choice_move = 999;
    [Space]
    [Header("* 호 크기")]
    [Range(-100, 5)]
    public float offset = 0.2f;
    [Header("* Slerp 이동 변수")]
    public float SL_count = 1000;

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

    public AttackableImp my_hit_data { get; set; }

    bool is_ready;
    enum MoveType
    {
        MoveTowad, SmoothDamp, Slerp
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 6) //"Target"레이어에 해당하는 오브젝트라면
        {
            DamageableImp target = collision.GetComponent<DamageableImp>();
            if (target != null)
            {
                target.OnDamage(my_hit_data.OnAttack(collision));
                Debug.Log("두번 반납하면 두번 뜨겠지");
            }
            //이펙트 실행 후
        }
        else if (collision.tag == "Plane")
        {
            ReleaseObject();

        }
    }

    IEnumerable<Vector3> SlerpMoving(Vector3 start, Vector3 end, float center_offset)
    {
        var center_pivot = (start + end) * 0.5f;

        center_pivot -= new Vector3(0, -center_offset);

        var start_relative_center = start - center_pivot;
        var end_relative_center = end - center_pivot;

        var f = 1f / SL_count;

        for (var i = 0f; i < 1 + f; i += f)
        {
            
            yield return Vector3.Slerp(start_relative_center, end_relative_center, 0.05f) + center_pivot;
        }

    }

    //MoveTowrad  방식
    //Vector3.MoveTowards(현재 위치, 목표 위치, 속력)
    //직선으로 이동하는 방식 
    // movement will not overshoot the target
    //현재위치에서 목표위치 방향으로 속력만큼 움직인다
    [Header("* MoveTowrad 이동 변수")]
    public float speed = 999;

    void MoveTowradMoving()
    {
        if (speed == 999) { Debug.LogError("MoveTowrad에 speed 값 없음"); return; }

        if(target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target_none.position, step);
        }
        
    }

    //SmoothDamp 방식
    //Vector3.SmoothDamp(현재 위치, 목표 위치, 참조 속력, 소요 시간)
    //부드럽게 이동, 어느정도 포물선도 가능하다고 판단
    //일반적인 용도: 카메라를 매끄럽게 움직일 때
    [Header("* SmoothDamp 이동 변수")]
    public float smooth_time = 999; //목표에 도달하는 데 걸리는 대략적인 시간
    private Vector3 velocity = Vector3.zero;
    void SmoothDampMoving()
    {
        if (smooth_time == 999) { Debug.LogError("SmoothDamp에 smooth_time 값 없음"); return; }
        if(target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smooth_time);

        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, target_none.position, ref velocity, smooth_time);

        }
        //ref velocity 호출할때 마다 함수에 의해 수정 됨
    }

    private Vector3 startPos, endPos;
    //땅에 닫기까지 걸리는 시간
    protected float timer;
    protected float timeToFloor;


    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    protected IEnumerator BulletMove()
    {
        timer = 0;
        while (transform.position.y >= startPos.y)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 5, timer);
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
        }
    }

    private void OnDisable()
    {
        if (ObjectPoolManager.instance.IsReady)
        {
            transform.position = init_transform.position;
            StopCoroutine("BulletMove");
        }
    }

}
