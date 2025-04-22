using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillComponent : MonoBehaviour
{
    public abstract void UseSkill();
}

public class HealSkill : SkillComponent
{
    public override void UseSkill()
    {
        Debug.Log("회복 스킬");
    }
}
public class RangedSkillComponent : SkillComponent
{
    public override void UseSkill()
    {
        Debug.Log("원거리 스킬");
    }
}

public class InvincibilitySkill : SkillComponent
{
    public override void UseSkill()
    {
        Debug.Log("무적 스킬");
    }
}