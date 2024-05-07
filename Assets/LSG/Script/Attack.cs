using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Attack : MonoBehaviour
{

    string my_parent_name;
    //public GameObject my_effect_obj;
    private float my_cool_time;
    float my_attack_distance;//==>근거리냐 원거리냐? 피격범위?
    float my_skill_distance;//==>근거리냐 원거리냐? 피격범위?
    public float deg; //포물선 각도

    public IObjectPool<GameObject> bullet_pool { get; set; }

    public enum AttackType{ Noaml, Skill };

     AttackableImp parent_attack_data;
    SkillUserImp parent_skill_data;

    //UI 에서는 얘를 인식해서 버튼 누를 때 마다 스킬 쓸 수 있게 코드 짤 것
    public AttackType my_attack_type
    {
       get { return my_attack_type; }
        set
        {
            switch (value)
            {
                //자동으로 할 때 코루틴으로 변경해둘 것 
                case AttackType.Noaml: StartCoroutine(AttackFire()); break; //==> 코루틴으로 작업 구분 . 노말에선 루프 돌리고 스킬 쓸 때 nomal에서 돌리던 코루틴 정지
                case AttackType.Skill: StopCoroutine(AttackFire()); StartCoroutine(SkillFire()); break;
            }
        }
    }
    
    void InitData(Cat _my_data) 
    {
        //Cat에 Attack있어야할듯
        parent_attack_data = _my_data.GetComponent<AttackableImp>(); 
        if (parent_attack_data != null)
        {
            my_cool_time = parent_attack_data.atk_time;
            //UnityEngine.Debug.Log(my_parent_name + "쿨타임: " + my_cool_time);

            my_attack_distance = parent_attack_data.atk_distance;
        }
        else
        {
            UnityEngine.Debug.LogError("공격을 위한 고양이 데이터 가져오기 실패");
        }

        parent_skill_data = _my_data.GetComponent<SkillUserImp>();
        if(parent_skill_data != null)
        {
            my_skill_distance = parent_skill_data.skill_distance;
        }
        //현재 공격이랑 데이터 동일
    }

    void InitData(Tower _my_data)
    {

        parent_attack_data = _my_data.GetComponent<AttackableImp>();
        if (parent_attack_data != null)
        {
            my_cool_time = parent_attack_data.atk_time;
            //UnityEngine.Debug.Log(my_parent_name + "쿨타임: " + my_cool_time);
            my_attack_distance = parent_attack_data.atk_distance;
        }
        else
        {
            UnityEngine.Debug.LogError("공격을 위한 타워 데이터 가져오기 실패");
        }
    }

    private void Start()
    {
        my_parent_name = transform.parent.name;
        var Catdata = gameObject.GetComponentInParent<MyHeroesImp>();
        var Towerdata = gameObject.GetComponentInParent<Tower>();

        //데이터 가져오는걸 계속 체크해야할 듯 
        if (Catdata != null)
        {
            InitData(Catdata.GetMyData());
        }
        if (Towerdata != null)
        {
            InitData(Towerdata);

        }
        my_attack_type = AttackType.Noaml;
    }

    IEnumerator AttackFire()
    {
        yield return new WaitForSeconds(my_cool_time);
        while (true)
        {
            if (my_attack_distance > 5 && ObjectPoolManager.instance.IsReady)
            {
                //이걸 줄이는 방법이 없을까?
                //원거리
                var my_bullet_obj = ObjectPoolManager.instance.GetGo(my_parent_name + "_Atk_Obj");
                my_bullet_obj.GetComponent<BulletImpact>().MyHitData(parent_attack_data);
                my_bullet_obj.GetComponent<BulletImpact>().init_transform = this.transform;
                my_bullet_obj.transform.position = transform.position;
                my_bullet_obj.GetComponent<BulletImpact>().my_speed = parent_attack_data.speed;
            }
            else if(my_attack_distance < 5 && ObjectPoolManager.instance.IsReady)
            {
                //근거리
                var my_bullet_obj = ObjectPoolManager.instance.GetGo(my_parent_name + "_Atk_Obj");
                my_bullet_obj.GetComponent<MeleeImpact>().MyHitData(parent_attack_data);
                my_bullet_obj.GetComponent<MeleeImpact>().init_transform = this.transform;
                my_bullet_obj.transform.position = transform.position;
            }
            else { Debug.Log("데이터 전달 실패"); //데이터 전달 실패할때 어쩌면 좋을까?
                continue;
            }

            yield return new WaitForSeconds(my_cool_time);
        }
    }

    //버튼 누를 때 실행 이거 넘겨주는게 문제인데 어떻게 처리할까
    IEnumerator SkillFire()
    {
        //아래 방법x 각 캐릭터마다 스킬을 날리는 거랑 아닌거랑 여러가지니까 해당 캐릭터 공격 불러오는걸로
        Debug.Log(my_skill_distance);
        if (my_skill_distance > 5)
        {
            var my_bullet_obj = ObjectPoolManager.instance.GetGo(my_parent_name + "_Skill_Obj");
            //원거리
            Debug.Log(parent_skill_data);
            my_bullet_obj.GetComponent<BulletImpact>().MyHitData(parent_skill_data); //데이터가 안가져와지나?
            my_bullet_obj.GetComponent<BulletImpact>().init_transform = this.transform;
            my_bullet_obj.GetComponent<BulletImpact>().my_speed = parent_attack_data.speed;

            my_bullet_obj.transform.position = transform.position;
        }
        else if (my_skill_distance < 1)
        {
            //서포트계열
            Collider2D collider = null;
            parent_skill_data.OnSkill(collider);
        }
        else if(my_skill_distance > 1 && my_skill_distance < 5)
        {
            //근거리 공격
            var my_bullet_obj = ObjectPoolManager.instance.GetGo(my_parent_name + "_Skill_Obj");

            my_bullet_obj.GetComponent<MeleeImpact>().MyHitData(parent_skill_data);
            my_bullet_obj.GetComponent<MeleeImpact>().init_transform = this.transform;
            my_bullet_obj.transform.position = transform.position;
        }
        else
        {
            Debug.Log("데이터 세팅하기 실패");
        }
        yield return new WaitForSeconds(my_cool_time);
        my_attack_type = AttackType.Noaml;
        yield return null;
    }
}
