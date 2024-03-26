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

    private Transform init_transform;
    LookTarget found_target_obj;

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
            }
            //이펙트 실행 후
            ReleaseObject();
            transform.position = init_transform.position;
        }
        if (collision.tag == "Plane")
        {
            ReleaseObject();
            transform.position = init_transform.position;
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
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
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

        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smooth_time);
        //ref velocity 호출할때 마다 함수에 의해 수정 됨
    }



    private void Start()
    {
        init_transform = transform;
        found_target_obj = FindObjectOfType<LookTarget>(); //타겟 거리별로 감지하는 오브젝트 찾음 =>본인위치랑 비교함
        if (found_target_obj == null) UnityEngine.Debug.Log("found_target_obj 찾을 수 없음");
    }

    private void Update()
    {
        if (choice_move == 999) { Debug.LogError("choice_move 값 없음"); return; }

        switch (choice_move)
        {
            case (int)MoveType.MoveTowad:
                MoveTowradMoving();
                break;
            case (int)MoveType.SmoothDamp:
                SmoothDampMoving();
                break;
            case (int)MoveType.Slerp:
                foreach (var point in SlerpMoving(transform.position, target.position, offset))
                {

                    transform.position = point;
                }
                break;
        }
    }

}
