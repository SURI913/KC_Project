using PlayFab;
using PlayFab.ClientModels;
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

        Debug.Log("버튼 눌러짐,게임매니저");
      
    }
    public Monster monster;
    public MonsterData monsterData;
    public int check;
    public void SetMonsterDataByIndex(int index)
    {
        Debug.Log(" 1111");
        Debug.Log(index);
        if (index >= 1 && index <= monsterData.dungeon_monsterdatas.Length)
        {
            MonsterD stageData = monsterData.dungeon_monsterdatas[index - 1];
            Debug.Log(stageData);

            monster.SetMonsterData(stageData);
           
        }
        else
        {
            Debug.LogError("Invalid stage index: " + index);
            Debug.Log(" 1111");
        }
    }
    public void Dungeon_Play_UI(bool active)
    {
        //타이머 활성화호ㅏ

    }    
   private int Monster_data_index;
    
    public void Monster_Data_index(int index)
    {
        Monster_data_index = index;
        // _monster.SetMonsterDataByIndex(Monster_data_index);
        //Monster스크립트 참조해서 SetMonsterDataByIndex(Monster_data_index); 로 불러오면 될거같은뎅

    }

    //-------------------------------------------------------------------------스테이지 관리
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

    //----------------------------------------------------------------------로그인 & 유저 데이터 관리
    public string playfabID;
    public float ex;
    public float consume_ex;
    public int lv;
    public string current_stage; //처음 데이터 초기화 할때쓰일거임

    public void InitUserData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"Lv", "1"},
             { "CurrentStage", "1-1"}
        }
        },
        result => Debug.Log("Successfully updated user data"),
        error => {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    public void UploadUserDataTowerLv()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"Lv", lv.ToString()},
            {"Ex", ex.ToString() },
            {"CurrentStage",current_stage}
        }
        },
        result => Debug.Log("Successfully updated user data"),
        error => {
            Debug.Log("Got error setting user data");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    public void UploadUserDataCurrentStage()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"CurrentStage",current_stage}
        }
        },
        result => Debug.Log("Successfully updated user data"),
        error => {
            Debug.Log("Got error setting user data");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    public void GetUserData(string myPlayFabId)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        }, result => {
            Debug.Log("Got user data");
            if (result.Data == null || !result.Data.ContainsKey("Lv")) Debug.Log("No Ancestor");
            else
            {
                lv = int.Parse(result.Data["Lv"].Value);
                ex = float.Parse(result.Data["Ex"].Value);
                current_stage = result.Data["CurrentStage"].Value;
            }
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void ClientGetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
            result => {
                if (result.Data == null || !result.Data.ContainsKey("MonsterName")) Debug.Log("No MonsterName");
                else Debug.Log("MonsterName: " + result.Data["MonsterName"]);
            },
            error => {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }
    
}
