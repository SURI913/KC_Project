using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletImpact : PoolAble
{
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 6) //"Target"레이어에 해당하는 오브젝트라면
        {
            /*IDamageable target = collision.GetComponent<IDamageable>();
            if(target != null)
            {
                
            }*/
            //이펙트 실행 후
            ReleaseObject();
            transform.position = init_transform.position;
        }
    }

    private void Start()
    {
        init_transform = transform;
        found_target_obj = FindObjectOfType<LookTarget>(); //타겟 거리별로 감지하는 오브젝트 찾음 =>본인위치랑 비교함
        if (found_target_obj == null) UnityEngine.Debug.Log("found_target_obj 찾을 수 없음");
    }

    [Space]
    [Header("* X *")]

    [Header("* 호 크기")]
    [Range(-100, 5)]
    public float offset;
    [Header("* Slerp 이동 변수")]
    public float SL_count = 1000;
    //총알 공격 그림
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

    private void Update()
    {
        // 총알이 많이 날라가면 삭제 해주기
        if (target != null)
        {
            foreach (var point in SlerpMoving(transform.position, target.position, offset))
            {
                transform.position = point;
            }
        }
        else
        {
            foreach (var point in SlerpMoving(transform.position, new Vector3(0, -10, 0), offset)) //고정된 위치에다가 공격
            {
                transform.position = point;
            }
        }

        if (this.transform.position.y < -20)
        {
            // 오브젝트 풀에 반환
            ReleaseObject();
            transform.position = init_transform.position;
        }
    }
}
