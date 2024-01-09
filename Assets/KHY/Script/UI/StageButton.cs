using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StageButton : MonoBehaviour
{
    public int stageIndex; // 각 버튼에 해당하는 스테이지 번호를 인스펙터에서 설정
    //public Monster monster; // M_D01 스크립트를 인스펙터에서 할당

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
       
    }
  
    public void OnButtonClick()
    {
        Monster _monster = GameObject.FindWithTag("Enemy").GetComponent<Monster>();
        //스테이지 인덱스틑 저장해서 보냄
        if (_monster != null)
        {
            int rowIndex = stageIndex;

            MonsterData tmpData = _monster.monsterData;

            if (rowIndex >= 1 && rowIndex <= tmpData.monsterdatas.Length)
            {

                _monster.SetMonsterDataByIndex(rowIndex);
            }
            else
            {
                Debug.LogError("Invalid stage index: " + stageIndex);
            }

           
        }
     
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
