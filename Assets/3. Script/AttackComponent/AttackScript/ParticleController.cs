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

    private void OnEnable()
    {
        if (myParticle != null) myParticle.Play();
    }

    private void OnParticleSystemStopped()
    {
        ReleaseObject();
    }
}
