using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StageButton : MonoBehaviour
{
    public int stageIndex; // 각 버튼에 해당하는 스테이지 번호를 인스펙터에서 설정
    public Monster monster; // M_D01 스크립트를 인스펙터에서 할당

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
       
    }
  
    public void OnButtonClick()
    {
        //스테이지 인덱스틑 저장해서 보냄
        if (monster != null)
        {
            int rowIndex = stageIndex;

            if (rowIndex >= 1 && rowIndex <= monster.monsterData.monsterdatas.Length)
            {
                monster.SetMonsterDataByIndex(rowIndex);
            }
            else
            {
                Debug.LogError("Invalid stage index: " + stageIndex);
            }
        }
        /*
                if (monster != null)
                {

                    int rowIndex = stageIndex;

                    if (rowIndex >= 1 && rowIndex <= monster.monsterData.monsterdatas.Length)
                    {
                        // 해당 스테이지에 대한 데이터를 가져오고 처리
                        //MonsterD 에서 저장한 리스트를 접근 (데이터가 저장되어있음)
                        //스프레드시트는 행이 1부터 시작함 그래사 rowindex -1
                         MonsterD stageData = monster.monsterData.monsterdatas[rowIndex - 1];
                       //  monster.SetMonsterData(stageData);

                        // 스테이지 데이터를 사용하여 몬스터의 속성을 설정
                    *//*    monster.stageID = stageData.stageID;
                        monster.HP = stageData.hp;
                        monster.Attack = stageData.attack;
                        monster.AtkTime = stageData.atktime;*//*

                        //확인을 위한 출력 
                        Debug.Log("Clicked stage button with stageIndex: " + stageIndex);
                        Debug.Log("Stage " + stageIndex + " - ID: " + monster.stageID + "," +
                            " HP: " + monster.HP + ", Attack: " + monster.Attack + ", Attack Time: " + monster.AtkTime);

                    }
                    else
                    {
                        Debug.LogError("Invalid stage index: " + stageIndex);
                    }
                }*/
        //클릭하고 입장하기 버튼 누르면 씬 넘기기
        // ENTER 스크립트 하나 짜자 그냥 , 로드 씬만 하기로 
    }
}
//지피티
/*, 위에서 제공한 StageButton 스크립트를 UI 버튼에 추가하고 각 버튼에 해당하는 
 * 스테이지 번호를 할당하면 됩니다. 이 스크립트는 각 스테이지 버튼이 클릭되면 
 * 해당 스테이지 데이터를 가져오고 몬스터에 할당하는 역할을 합니다. 물론 스테이지 버튼과 
 * 몬스터 스크립트 간의 상호 작용을 위해 인스펙터에서 각 버튼에 대한 M_D01 몬스터 스크립트를 할당해야 합니다.

버튼을 클릭할 때 해당 스테이지의 데이터를 가져오고 몬스터의 속성을 설정하려면
위에서 제공한 스크립트를 사용하시면 됩니다. 이 스크립트가 원하는 동작을 수행할 것입니다.
*/
