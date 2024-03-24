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
    

    [SerializeField]
    public Canvas Enterstage;
    public void Dungeon_Start_UI(bool active)
    {
        Enterstage.gameObject.SetActive(!active);
    }
   private int Monster_data_index;
   
    public void Monster_Data_index(int index)
    {
        Monster_data_index = index;
        // _monster.SetMonsterDataByIndex(Monster_data_index);
        //Monster스크립트 참조해서 SetMonsterDataByIndex(Monster_data_index); 로 불러오면 될거같은뎅


    }
    //던전 
    //씬관리 ,
}
