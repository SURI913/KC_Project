using Spine;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //아니면 에셋번들 사용하는 방향 고려
    public GameObject my_attack_obj;
    //public GameObject my_effect_obj;
    float my_cool_time;
    float my_speed; //==>애니메이션
    float my_attack_distance;//==>근거리냐 원거리냐?

    LookTarget found_target_obj;

    Transform target {
        get {
            if (found_target_obj.colliders.Length > 0)
            {
                return found_target_obj.colliders[0].transform;
            }
            else return null;
        }
            
    }  //타겟 위치, 자식 위치로 가져와야함

    public enum AttackType{ Noaml, Skill }

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
            UnityEngine.Debug.LogError("공격을 위한 고양이 데이터 가져오기 실패");
        }
    }

    private void Start()
    {
        found_target_obj = FindObjectOfType<LookTarget>(); //타겟 거리별로 감지하는 오브젝트 찾음 =>본인위치랑 비교하ㅡㄴㄱ
        //pool = new Pool<GameObject>(new BulletPrefabFactory<GameObject>(prefab), 10);
        if (found_target_obj == null) UnityEngine.Debug.Log("found_target_obj 찾을 수 없음");
        var Catdata = gameObject.GetComponentInParent<MyHeroesImp>();
        var Towerdata = gameObject.GetComponentInParent<Tower>();
        if (Catdata != null)
        {
            InitData(Catdata.GetMyData());
        }
        else if(Towerdata != null)
        {
            InitData(Towerdata);

        }
        SL_interpolation_length = 0.001f;

        ObjectPoolManager.instance.GetGo(transform.parent.name+"_Atk");
    }

    [Header("* Slerp 이동 변수")]
    [Range(-10,10)]
    public float SL_interpolation_length = 999; //보간 값이 작을수록 부드러운 움직임
    void SlerpMoving()
    {
        if (SL_interpolation_length == 999) { UnityEngine.Debug.LogError("Slerp에 SL_interpolation_length 값 없음"); return; }
        if (target != null)
        {
            transform.position = Vector3.Slerp(transform.position, target.position, SL_interpolation_length);
        }
        //가장 먼저 정렬된 타겟에게 
    }


    private void Update()
    {
        //거리정렬해서 가장 가까운애한테 공격 하게끔
        SlerpMoving();
    }
}
