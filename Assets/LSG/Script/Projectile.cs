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
    protected State state {  get; set; }    //현재 발사체 상태

    protected GameObject bullet;    // 총알
    protected int bulletCount;    // 총알
    protected Transform fireTransform;  //발사될 위치
    protected LineRenderer bulletLineRenderer;  //총알 궤적을 그리기는 용
    protected Rigidbody2D rb;

    protected string ID;    //캐릭터 넘버
    protected float Cooltime = 999;    //스킬 쿨타임
    protected double attack = 0;  //공격력
    protected float speed = 0;  //속력

    private void Awake()
    {
        rb = bullet.GetComponent<Rigidbody2D>();
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
        state = 0;
    }

    //총알 생성 및 발사
    protected void Fire()
    {
        Instantiate(bullet, fireTransform);
        rb.AddForce(transform.forward * speed, ForceMode2D.Impulse);
    }

    protected void FrieRay()
    {

    }
}
