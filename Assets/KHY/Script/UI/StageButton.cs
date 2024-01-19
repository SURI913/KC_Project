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
        //??????뭐지 여기서 뭔 오류지????????????????????????????????? 
        //게임매니저 생성 , 씬 분리 후 갑자기 오류남 
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
