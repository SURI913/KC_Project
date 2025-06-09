using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillComponent : MonoBehaviour
{
    public string characterId;
    public abstract void UseSkill(Vector2 targetPostion);
}

