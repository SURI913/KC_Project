using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour
{
    GameObject inventory_obj;
    private void Awake()
    {
        inventory_obj = GameObject.Find("InventoryCanvas");
    }
    enum ChangeUIFuntion
    {
        ShowMainUI, ShowTower, ShowDungeonUI, Inventory
    }

    [Tooltip ("전환할 씬 인덱스 번호")]
    public int change_ui;


    private ScenesManager sceneManager;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeScene);
        sceneManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
    }

    void ChangeScene()
    {

        switch (change_ui)
        {
            case (int)ChangeUIFuntion.ShowMainUI: { StartCoroutine(sceneManager.ShowMainUI()); break; }
            case (int)ChangeUIFuntion.ShowTower: { StartCoroutine(sceneManager.ShowTower()); break; }
            case (int)ChangeUIFuntion.ShowDungeonUI: { StartCoroutine(sceneManager.ShowDungeonUI()); break; }
            default: { break; }
        }
    }
}
