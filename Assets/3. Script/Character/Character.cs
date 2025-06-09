using UnityEngine;
using UnityEngine.AddressableAssets;

public class Character : BattleUnit
{

    private CharacterDataBase characterData; //현재 캐릭터 위치에 들어가는 캐릭터 정보
    private SkillBehaviourSO skillBehaviour;
    [SerializeField] private AttackComponent attackComponent;

    [SerializeField] private Transform modelRoot; //모델 생성위치
    private GameObject currentModel;


    public GameObject targetObject; //이후에 타겟 인식하는 오브젝트 새로 생성할 것 그 친구 참조하자
    private bool isActiveSkill;

    public float attackCooltime = 2f;
    private float attackTime = 0f;

    private void Awake()
    {
        //recoveryComponent = GetComponent<RecoveryComponent>();
        if (targetObject == null) targetObject = gameObject;
        isActiveSkill = false;
    }

    //게임 시작할 때, 캐릭터 변경 시 적용
    public async void ApplyCharacter(CharacterDataBase newDataBase)
    {
        characterData = newDataBase;
        myMotion = GetComponentInChildren<Animator>();

        // 기존 모델 제거
        if (currentModel != null)
            Destroy(currentModel);

        //리깅된 외형 프리팹은 Addressable로 비동기 로드
        var prefab = await Addressables.LoadAssetAsync<GameObject>(newDataBase._model_address).Task;
        currentModel = Instantiate(prefab, modelRoot);
        currentModel.transform.localPosition = Vector3.zero;

        // 이벤트 연결
        var relay = currentModel.GetComponent<AvatarEventRelay>();
        relay.Init(this); // 자기 자신(this)을 넘겨줌
        
    }

    private void Update()
    {
        attackTime += Time.deltaTime;

        if (attackTime >= attackCooltime)
        {
            myMotion.SetTrigger("isAttack"); //자동 공격
            attackTime = 0f;
        }
    }

    public void PerformAttack()
    {
        if (attackComponent != null)
            attackComponent.Attack(targetObject.transform.position);
    }

    //애니메이션 이벤트에서 호출
    public void PerformSkill()
    {
        if (skillBehaviour != null)
            skillBehaviour.UseSkill(this, targetObject.transform.position);
    }

}
