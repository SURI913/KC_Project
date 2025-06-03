using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffectAttack : AttackComponent
{
    // 일정 거리내에 적 처지 하는걸로
    // 타겟을 따로 지정하자
    public LayerMask targetMask;
    public Vector3 line;
    public Vector2 area;

    // Collider2D 배열 생성 (최대 20개의 콜라이더 저장)
    private Collider2D[] hitTargets = new Collider2D[20];

    //공격 시 이펙트 재생할때 필요한 변수
    public ParticleSystem myEffect;

    /*    private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position+ line, area);
        }*/

    public override void Attack(Vector2 not)
    {
        if(myEffect != null) myEffect.Play();
        HitTartget();
    }

    private void HitTartget()
    {
        //인식된 20개체만 데미지효과를 보냄
        int hitCount = Physics2D.OverlapBoxNonAlloc(transform.position + line, area, 0, hitTargets, targetMask);
        for(int i =0; i< hitCount; i++)
        {
            var hitObject = ObjectPoolManager.instance.GetGo(characterId + "_Attack_HitObject");
            hitObject.transform.position = hitTargets[i].transform.position;
        }
    } 
}
