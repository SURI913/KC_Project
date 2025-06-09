using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : PoolAble
{
    public  enum attackType { _Attack, _Skill}

    public string characterId;
    public attackType myType;
    private Rigidbody2D myRigdbody2D;
    private ParticleSystem myParticle;
    private ParticleSystem.MainModule myParticleMain;
    private Vector3 hitPostion;
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
            hitPostion = transform.position;
            if (collision.GetComponent<BattleUnit>())
            {
                collision.GetComponent<BattleUnit>().TakeDamage(damage);
            }
            Hit();
        }
    }

    private void Hit()
    {
        //공백ornull체크
        if (!string.IsNullOrWhiteSpace(characterId))
        {
            var hitObject = ObjectPoolManager.instance.GetGo(characterId + myType+"_HitObject");
            hitObject.transform.position = hitPostion;
        }
        ReleaseObject();
    }

    public void SetVelocity(Vector2 value)
    {

        if (myRigdbody2D != null) myRigdbody2D.velocity = value;
    }

    public void SetParticleRotate(float angle)
    {
        if (myParticle != null) {
            myParticleMain.startRotation = new ParticleSystem.MinMaxCurve(angle, angle);
        }
    }

    public void OnEnable()
    {
        if (myParticle != null) myParticle.Play();
    }

}
