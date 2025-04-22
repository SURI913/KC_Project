using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecoveryComponent : MonoBehaviour
{
    public float RecoveryAmount;
    public float Cooldown;
    public abstract void Recover();
}

// 패시브 체력 재생 (일정 시간마다 자동 회복)
public class PassiveRegen : RecoveryComponent
{
    private void Start()
    {
        InvokeRepeating(nameof(Recover), Cooldown, Cooldown); // 일정 시간마다 자동 회복
    }

    public override void Recover()
    {
        BattleUnit unit = GetComponent<BattleUnit>();
        if (unit != null)
        {
            unit.RecoverHealth(RecoveryAmount);
        }
    }
}

// 액티브 회복 (플레이어가 직접 사용)
public class ActiveHeal : RecoveryComponent
{
    public override void Recover()
    {
        BattleUnit unit = GetComponent<BattleUnit>();
        if (unit != null)
        {
            unit.RecoverHealth(RecoveryAmount);
        }
    }
}
