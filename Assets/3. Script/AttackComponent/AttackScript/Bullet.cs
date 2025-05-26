using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : PoolAble
{
 
    private Rigidbody2D myRigdbody2D;
    private ParticleSystem myParticle;
    private ParticleSystem.MainModule myParticleMain;
    public float damage;
    
    private void Awake()
    {
        myRigdbody2D = GetComponent<Rigidbody2D>();
        myParticle = GetComponent<ParticleSystem>();
        if (myParticle != null) myParticleMain = myParticle.main;
    }

    void Update()
    {
        transform.right = myRigdbody2D.velocity;
    }

    //바닥에 닿거나 적 피격시 총알 반납
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.Pool != null && (collision.CompareTag("Plane") || collision.CompareTag("Target")))
        {
            if (collision.GetComponent<BattleUnit>())
            {
                collision.GetComponent<BattleUnit>().TakeDamage(damage);
            }

            ReleaseObject();
        }
    }

    public void SetVelocity(Vector2 value)
    {

        if (myRigdbody2D != null) myRigdbody2D.velocity = value;
    }

    public void SetParticleRotate(float angle)
    {
        if (myParticle != null) {
            print(angle); //래디언으로 들어가야하나?
            myParticleMain.startRotation = new ParticleSystem.MinMaxCurve(angle, angle);
        }
    }

    public void OnEnable()
    {
        if (myParticle != null) myParticle.Play();
    }

}
