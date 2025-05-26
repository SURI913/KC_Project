using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackComponent : MonoBehaviour
{
   public string characterId;
   public abstract void Attack(Vector2 targetPostion);
}

public class MagicAttackComponent : AttackComponent
{
    public override void Attack(Vector2 targetPostion)
    {
        Debug.Log("마법 공격");
    }
}
