using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //원거리 공격용
    protected enum State
    {
        Ready,   //발사준비
        Empty,  //발사 완료
        Reloading   //쿨타임 대기중
    }
    protected string ID;
    protected State state { get; set; } = State.Ready;    //현재 발사체 상태
    public bool isAttack { get; set; }

    protected GameObject bullet;    // 총알 //
    protected Transform fireTransform;  //발사될 위치
    protected Rigidbody2D rb;       //총알 리지드바디

    protected GameObject grandParent;   //Attack or Skill 에서 부모 오브젝트 저장
    protected IAttack grandParentIAttack;

    protected GameObject newBullet;

    protected RaycastHit2D target;

    //부채꼴 범위를 위한 변수
    public float angleRange = 30f;
    public float radius = 3f;

    protected virtual void Fire()   //총알 생성 및 발사
    {
      
        if (!grandParent.GetComponent<Cat>().dead) //update 부하걸릴 가능성
        {
            //Debug.Log(ID+"가 타겟을 찾음"+target.collider.name);
            Vector2 Pos = target.transform.position - fireTransform.position;
            newBullet = Instantiate(bullet, fireTransform.position, Quaternion.identity);
            rb = newBullet.GetComponent<Rigidbody2D>();
            rb.velocity = Pos.normalized * grandParentIAttack.speed;
            state = State.Empty;
        }
    }

    protected virtual void OnBullet() //스킬이면 오버라이드
    {
        IDamageable hitDamage = target.collider.GetComponent<IDamageable>();
        if (hitDamage != null) //Damageaable을 쓰고있다면
        {
            //Debug.Log(ID + "공격중");
            hitDamage.OnDamage(grandParentIAttack.OnAttack(target), target);
            Destroy(newBullet, 2f);   //2초 뒤 파괴
            //hit된 오브젝트에 자식 Attack값만큼 데미지입힘
        }
        {
            //Debug.Log(ID + "대상이 없습니다");
        }
    }
    bool checkTarget = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(target.point.x, target.point.y, 0), transform.lossyScale * 20);
        if (target)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + new Vector3(target.point.x, target.point.y, 0), transform.lossyScale * 20);

        }
    }

    protected void StateCheck()
    {
        if (!target)
        {
            checkTarget=false;
            //Debug.Log(ID + "타겟이 없습니다");
        }
        if (!checkTarget)
        {
            target = Physics2D.BoxCast(fireTransform.position, transform.lossyScale*20, 0f, Vector2.right,20f, LayerMask.GetMask("Target"));

            checkTarget = true;
        }
        //이것도 적마
        //다 방향 생각해서 작업해야하나
        //일반 공격
        
        if(target)
        {
            if (state == State.Ready) //장전완료
            {
                //Debug.Log("생성");
                Fire();

            }
            if (state == State.Empty )
            { //데미지 체크
              //Debug.Log("데미지");

                OnBullet();
                state = State.Reloading;
            }
            //처치 완료 했을 경우 로딩상태로
        }

    }

}
