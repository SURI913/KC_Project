using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    protected State state {  get; set; }    //현재 발사체 상태

    protected GameObject bullet;    // 총알 //
    protected int bulletCount;    // 총알 //
    protected Transform fireTransform;  //발사될 위치
    protected Rigidbody2D rb;       //총알 리지드바디

    protected float Cooltime = 999;    //스킬 쿨타임 //

    protected IAttack ChildIAttack; //

    private void Awake()
    {
        rb = bullet.GetComponent<Rigidbody2D>();
        ChildIAttack = this.transform.parent.GetComponent<IAttack>(); //캐릭터에다가 넣어줘야하는 스크립트
    }

    //총알 생성 및 발사
    protected void Fire()
    {
        Instantiate(bullet, fireTransform);
        rb.AddForce(transform.forward * ChildIAttack.speed, ForceMode2D.Impulse);
    }

    protected void OnBullet() //스킬이면 오버라이드
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 1);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            hit.collider.GetComponent<IDamageable>().OnDamage(ChildIAttack.OnAttack(hit), hit);
            //hit된 오브젝트에 자식 Attack값만큼 데미지입힘
        }
    }

    void FixedUpdate()
    {
        //일반 공격
        if (state == State.Ready) //장전완료
        {
            Fire();
            state = State.Empty;
        }
        if (state == State.Empty) { //데미지 체크
            OnBullet();
            state = State.Reloading;
        }
        if(Cooltime <= 0) {
            state = State.Ready;
        }
        Cooltime -= Time.deltaTime;
    }

}
