using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;
using UnityEngine.UI;

public class Cat : MonoBehaviour, IDamageable
{
    public string ID { get; protected set; }    //캐릭터 넘버
    protected int Lv = 0;  //레벨

    protected float xhp;      //체력배수
    protected float hpIncrease;  //레벨에 따른 체력배수 증가값

    public double hp { get; set; }      //체력
    public double maxHp { get; protected set; }   //최대체력
    protected float xattack;  //공격력배수
    protected float attackIncrease;  //레벨에 따른 공격배수 증가값

    protected float xprotect; //방어력X
    protected float xprotectIncrease; //방어력X
    protected float xhealing; //회복력배수
    protected float xhealingIncrease ; //회복력배수

    protected float criticalHit = 1f; //치명타율
    protected float criticalDamage = 0; //치명타데미지 

    protected float bossAttack = 0;  //보스 데미지 피해량 

    protected bool passiveAct = false; //패시브 활성화 유무
    public bool dead { get; set; } = false;    //죽음확인
    protected GrowingData growingData;

    //장비 멀로 처리하냐

    //-----------------------------------------------------------------------------------------------------------애니메이션
    protected Animator catMotion;    

    public float respawnTime = 8f;

    protected void hpInit(){    //체력 초기화
        if(!growingData)
        {
            Debug.Log(ID + "hp error!");
        }
       else
       {
            hp = maxHp;
            dead = false;
            catMotion.SetBool("isDead", false);
       }
            //체력이 0보다 작을 경우 초기화가 실행 되어야함
    }

    //캐릭터 데미지를 입히고 싶을 때 레이캐스트 판정(tag) Cat걸리면 실행시키면 됨 당사자의 데미지를 까고 0이면 hit 캐릭터 삭제?
    public void OnDamage(double Damage, RaycastHit2D hit) //캐릭터 데미지 입히는 호출
    {
        if (!dead)
        {
            //데미지 입음                 
            double damageAdd = Damage;//-(영웅 방어력*방어력(보유효과)*성급효과(뭔지모르겠음)*패시브스킬(유무))
            hp -= damageAdd;
            if (hp <= 0 && !dead) { Die(); }    //죽음처리
        }
        //패시브 스킬이 있는경우 오버라이딩으로 작업
    }

    protected virtual void healingApply(){
        //체력 회복
        if(xhealing == 0){
            Debug.Log("ID");
            Debug.Log("healing error!");
            return;
        }
        //영웅 회복력*회복력(보유효과)*장비장착효과(유무)*성급효과*별자리(유무)
    }

    public double attackApply(){
        //일반 공격값 반환
        if (xattack == 0){
            Debug.Log(ID);
            Debug.Log("attack error!");
            return 0;
        }
        if(growingData == null)
        {
            Debug.Log("공격 중 growingDataError!");

        }
        double AllAttack = growingData.Attack*xattack;
        Debug.Log(ID+(int)AllAttack);
        //영웅 공격력*공격력(보유효과)*성급효과*장비장착효과*패시브스킬*별자리
        //패시브 스킬은 어떻게 짤건지 고민 + 크리티컬 데미지 작업도 필요함
        //-----------------------------------------------------------------------------------------------------------애니메이션 추가
        return AllAttack;
    }

    //리스폰타임 뒤 체력 회복
    //애니메이션 처리도 여기서
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        catMotion.SetTrigger("FinishStune");
        hpInit();
    }

    private void Die()
    {
        dead = true;
        //캐릭터 죽는 모션
        catMotion.SetBool("isDead", true);
        StartCoroutine(Respawn());
    }

    private void UpdateData()
    {

        //작성
    }

    public void LevelUP()
    {
        if(ativelevelup)
        {
            Debug.Log(ID + "레벨업!");
            if (growingData == null)
            {
                Debug.Log("레벨업 중 growingData Error!");

            }
            Lv++;
            xattack += attackIncrease;
            xhp += hpIncrease;
            maxHp = growingData.Hp * xhp; //여기서 왜 오류남?
                                          //데이터 전달 후 값 업데이트
            ativelevelup = false;
        }
    }

    protected void printData()
    {
        Debug.Log(ID+" hp: "+Unit.ToUnitString(hp));
        Debug.Log(ID + "maxHp: " +Unit.ToUnitString(maxHp));
        Debug.Log(ID + "attack: " +Unit.ToUnitString(xattack));
        Debug.Log(ID + "Lv: " +Lv);
    }

    //일시적으로 제한
    private float cooltime = 10f;
    private bool ativelevelup = true;

    private void Update()
    {
        if (cooltime >= 0 && !ativelevelup)
        {
            cooltime -= Time.deltaTime;

        }
        else
        {
            ativelevelup = true;
            cooltime = 10f;
        }

    }
}
