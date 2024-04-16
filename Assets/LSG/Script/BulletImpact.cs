using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : PoolAble
{
    private Vector3 startPos, endPos;
    //땅에 닫기까지 걸리는 시간
    protected float timer;
    protected float timeToFloor;

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
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 6) //"Target"레이어에 해당하는 오브젝트라면
        {
            DamageableImp target = collision.GetComponent<DamageableImp>();
            if (target != null)
            {
                target.OnDamage(my_hit_data.OnAttack(collision));
                Debug.Log(name + "공격 나갔습니다."+ collision.name +"이 맞았습니다.");
                ReleaseObject();
                return;
            }
            //이펙트 실행 후
        }
        else if (collision.tag == "Plane")
        {
            Debug.Log(name + "공격 나갔습니다. 바닥이 맞았습니다.");

            ReleaseObject();           
        }
    }

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
