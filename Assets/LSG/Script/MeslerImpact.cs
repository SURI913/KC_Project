using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeslerImpact : PoolAble
{
    public ParticleSystem particle_object; //파티클시스템
    private void Awake()
    {
        particle_object = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        particle_object.Play();
    }
}
