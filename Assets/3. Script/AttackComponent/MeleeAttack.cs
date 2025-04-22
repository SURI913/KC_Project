using UnityEngine;

public class MeleeAttack : AttackComponent
{
    public override void Attack(GameObject target)
    {
        var my_bullet_obj = ObjectPoolManager.instance.GetGo(characterId + "_Atk_Obj");

        //my_bullet_obj.GetComponent<MeleeImpact>().MyHitData(parent_attack_data);
        Debug.Log("근접 공격");
    }
}