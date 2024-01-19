using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    // 플레이어, 일반스테이지 몬스터 추가



    //던전몬스터 
    public double DmonsterHP;
    public double DmonsterATK;
    public double DmonsterAtime;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    instance = singleton.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }
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


    //던전 
    //씬관리 ,
}
