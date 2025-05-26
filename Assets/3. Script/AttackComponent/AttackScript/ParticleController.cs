using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : PoolAble
{
    private ParticleSystem myParticle;
    public float damage;

    private void Awake()
    {
        myParticle = GetComponent<ParticleSystem>();
    }
    public void OnEnable()
    {
        if (myParticle != null) myParticle.Play();
    }
    public void OnParticleSystemStopped()
    {
        ReleaseObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.Pool != null && collision.CompareTag("Target"))
        {
            if (collision.GetComponent<BattleUnit>())
            {
                collision.GetComponent<BattleUnit>().TakeDamage(damage);
            }
        }

    }
}
