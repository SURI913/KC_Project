using Spine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Attack : MonoBehaviour
{
    //아니면 에셋번들 사용하는 방향 고려
    public GameObject my_attack_obj;
    public string my_parent_name;
    //public GameObject my_effect_obj;
    float my_cool_time;
    float my_speed; //==>애니메이션
    float my_attack_distance;//==>근거리냐 원거리냐?
    float time = 0;

    public IObjectPool<GameObject> bullet_pool { get; set; }

    public enum AttackType{ Noaml, Skill };

    public AttackType my_attack_type
    {
       get { return my_attack_type; }
        set
        {
            switch (value)
            {
                case AttackType.Noaml: break; //==> 코루틴으로 작업 구분 . 노말에선 루프 돌리고 스킬 쓸 때 nomal에서 돌리던 코루틴 정지
                case AttackType.Skill: break;
            }
        }
    }

    //타워는 타워 클래스받아오게하면끝
    //부모에서 값을 준다면? 근데 그 데이터를 전달받은걸 어떻게 확인함?
    
    void InitData(Cat _my_data) //생성자에서 값 전달 용?
    {
        print(my_parent_name + "초기화");
        //Cat에 Attack있어야할듯
        AttackableImp parent_attack_data = _my_data.GetComponent<AttackableImp>(); //접근방식 이거 아님?
        if (parent_attack_data != null)
        {
            my_cool_time = parent_attack_data.atk_time;
            my_speed = parent_attack_data.speed;
            my_attack_distance = parent_attack_data.atk_distance;
        }
        else
        {
            UnityEngine.Debug.LogError("공격을 위한 고양이 데이터 가져오기 실패");
        }
    }

    void InitData(Tower _my_data)
    {

        AttackableImp parent_attack_data = _my_data.GetComponent<AttackableImp>();
        if (parent_attack_data != null)
        {
            my_cool_time = parent_attack_data.atk_time;
            my_speed = parent_attack_data.speed;
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

    private void Update()
    {
        if (time < 0)
        {
            ObjectPoolManager.instance.GetGo(my_parent_name+ "_Atk_Obj");
            print(my_parent_name+"의 총알이 생성되었습니다.");
            time = my_cool_time;
        }
        time -= Time.deltaTime;
    }
}
