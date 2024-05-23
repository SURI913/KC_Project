using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 다른 씬으로 넘어갈 때 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }

    }

    //---------------------------------------------------------------------------------------------------------------------------던전 

    [SerializeField]
    public Canvas Enterstage;
    public Canvas StageSelect;
    public Canvas Storybox;
  
    public void Dungeon_Start_UI(bool active)
    {
        Enterstage.gameObject.SetActive(!active);
        StageSelect.gameObject.SetActive(!active);
        Storybox.gameObject.SetActive(!active);
       

        Debug.Log("버튼 눌러짐");
    }
   private int Monster_data_index;
   
    public void Monster_Data_index(int index)
    {
        Monster_data_index = index;
        // _monster.SetMonsterDataByIndex(Monster_data_index);
        //Monster스크립트 참조해서 SetMonsterDataByIndex(Monster_data_index); 로 불러오면 될거같은뎅


    }

    //----------------------------------스테이지 관리
    public float boss_gauge; //스테이지 넘어갈때 주의
    //=> 몬스터 생성 값 받아오자
    public bool is_clear_boss;
    public float monster_clear_count;

    public void ReSetClearCountData()
    {
        monster_clear_count = 0;
        is_clear_boss = false;
    }
    //보스 나오기 전까지는 처지한 몬스터 만큼 게이지를 조절해야함
    //보스 클리어 하고는 보스 체력바처럼 사용한다.
}
