using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class StageButton : MonoBehaviour
{ 
    public int stageIndex; // 각 버튼에 해당하는 스테이지 번호를 인스펙터에서 설정
    //public Monster monster; // M_D01 스크립트를 인스펙터에서 할당

    public Text recommendText;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);


        //텍스트 비활성화
       // recommendText.gameObject.SetActive(false);

    }

    public Text reco;
    public void OnButtonClick()
    {
       
        Monster _monster = GameObject.FindWithTag("Enemy").GetComponent<Monster>();
        // 씬이 넘어가면서 태그를 못찾는가봄 
        //스테이지 인덱스틑 저장해서 보냄
       

        if (_monster != null)
        {
            int rowIndex = stageIndex;
            
            MonsterData tmpData = _monster.monsterData;
          
            if (rowIndex >= 1 && rowIndex <= tmpData.dungeon_monsterdatas.Length)
            {
               // GameManager.instance.Monster_Data_index(1);//테스트 전달되는지 확인
                GameManager.instance.SetMonsterDataByIndex(rowIndex);
                Debug.Log("데이터 오나?");

                //d인덱스 값만 보내는데?



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
