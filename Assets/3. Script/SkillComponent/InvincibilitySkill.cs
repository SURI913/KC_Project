using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilitySkill : SkillComponent
{
    private BattleUnit myParent;
    public float time;
    //캐릭터 내에 있는 무적상태를 응용하는게 나을까? 새로 구현?
    public ParticleSystem myParticle;

    private void Awake()
    {
        myParent = GetComponentInParent<BattleUnit>();
    }

    public override void UseSkill(Vector2 not)
    {
        if(myParticle != null) myParticle.Play();
        if (myParent != null) StartCoroutine(myParent.Invincibility(time));
    }
    //무적중인 이펙트도 필요할것같은데 무적인 티가 나야지 알지
}
