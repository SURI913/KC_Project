using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarEventRelay : MonoBehaviour
{
    //부모가 가지고있을 공격, 스킬 작동트리거
    private Character owner;

    //모델 프리펩 생성된 후에 호출
    public void Init(Character owner)
    {
        this.owner = owner;
    }

    public void PerformAttack()
    {
        owner.PerformAttack();
    }
    public void PerformSkill()
    {
        owner.PerformSkill();
    }
}
