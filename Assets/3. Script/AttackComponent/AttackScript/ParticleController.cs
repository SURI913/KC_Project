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
}
