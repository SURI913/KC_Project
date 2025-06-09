using UnityEngine;

public abstract class SkillBehaviourSO : ScriptableObject
{
    public string _particle_ID;
    public abstract void UseSkill(BattleUnit myUint, Vector2 targetPostion);
}

[CreateAssetMenu(fileName = "HealSkillSO", menuName = "Skill / SkillSO / HealSkillSO")]
public class HealSkillSO : SkillBehaviourSO
{
    public override void UseSkill(BattleUnit myUnit, Vector2 not)
    {
        //스킬이펙트 재생해야하는데어케하징?
        Debug.Log("회복 스킬");
    }
}

public class InvincibilitySkillSO : SkillBehaviourSO
{
    public float time;
    public override void UseSkill(BattleUnit myUnit, Vector2 not)
    {
        if (_particle_ID != null)
        {
            var my_bullet_obj = ObjectPoolManager.instance.GetGo(_particle_ID);
            //위치 보정 필요 소켓처리
        }
        if (myUnit != null) myUnit.StartCoroutine(myUnit.Invincibility(time));
    }
    //무적중인 이펙트도 필요할것같은데 무적인 티가 나야지 알지
}

public class RangedSkillSO : SkillBehaviourSO
{
    /*
     * parabola :   speed = 20;
     * straight :   speed = 15~17, 중력X
     * 
     * 포물선 타겟은 사거리 맞춰서 고정할 것
     */
    public float speed;

    public override void UseSkill(BattleUnit myUnit, Vector2 targetPostion)
    {
        if (_particle_ID == null) return;
        if (myUnit != null) return;
        
        var my_bullet_obj = ObjectPoolManager.instance.GetGo(_particle_ID);

        Vector2 currentPostion = new Vector2(myUnit.transform.position.x, myUnit.transform.position.y);

        //바라볼 방향 = (도착할 위치 - 시작 위치).정규화
        Vector2 currentDirection = new Vector2(targetPostion.x - currentPostion.x, targetPostion.y - currentPostion.y).normalized;
        my_bullet_obj.transform.right = currentDirection;
        my_bullet_obj.transform.position = currentPostion;

        //== 타겟 방향으로 회전값 ==// 아크탄젠트로 각 구하고 라디언값 전달하면 자동으로 z 축에 디그리 값으로 변경됨
        float radian = Mathf.Atan2(currentDirection.y, currentDirection.x);
        var myBullet = my_bullet_obj.GetComponent<Bullet>();
        myBullet.SetVelocity(my_bullet_obj.transform.right * speed);
        myBullet.SetParticleRotate(-radian);
    }
}