using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeImpact : PoolAble
{

    public ParticleSystem particle_object; //파티클시스템

    public IAttack my_attack_data { get; set; }
    public Transform init_transform { get; set; }

    
    bool is_loop = false; //true = attack, false = skill
    public void  MyHitData(IAttack my_data)
    {
        if(my_data != null)
        {
            is_loop = true;
            transform.position = my_data.my_attack_transform.position;
        }
    }

    private void Awake()
    {
        particle_object = GetComponent<ParticleSystem>();
    }


    private void OnEnable()
    {
        if (particle_object != null)
            particle_object.Play();
    }
}
