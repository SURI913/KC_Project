using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : SkillComponent
{
    
    public override void UseSkill(Vector2 not)
    {
        //스킬이펙트 재생해야하는데어케하징?
        Debug.Log("회복 스킬");
    }
}
