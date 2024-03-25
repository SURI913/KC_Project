using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerItemUISet : MonoBehaviour
{
    public TowerData towerData;

    //아이템 배치
    public GameObject cannonScollBar;
    public GameObject repairmanScollBar;

    TowerItem[] Setcannon;
    TowerItem[] Setrepairman;


    private void Awake()
    {
        if (towerData != null)
        {
            // 데이터 저장할 곳 가져오기
            Setcannon = cannonScollBar.GetComponentsInChildren<TowerItem>();
            Setrepairman = repairmanScollBar.GetComponentsInChildren<TowerItem>();
            
            if(Setcannon.Length <= 0) { Debug.Log("UIlayout 체크"); return; }
            //데이터 보내기
            for(int i = 0; i < towerData.Cannon.Length; i++)
            {
                SetItem(Setcannon[i], towerData.Cannon[i]);  
            }
            for (int i = 0; i < towerData.RepairMan.Length; i++)
            {
                SetItem(Setrepairman[i], towerData.RepairMan[i]);
            }
            Setcannon[0].Ative = true;
            Setrepairman[0].Ative = true;
            Setcannon[0].ChoiceItem = true;
            Setrepairman[0].ChoiceItem = true;
        }
    }

    void SetItem(TowerItem target, CannonData getData)
    {
        target.id = getData.id;
        target.effect1 = getData.attackX;
        target.effect2 = getData.protectionX;
        target.increase1 = getData.attackX_increase;
        target.increase2 = getData.protectionX_increase;
        target.retention_effect1 = getData.retention_attack;
        target.retention_increase1 = getData.retention_attack_increase;
        target.retention_effect2 = getData.retention_protection;
        target.retention_increase2 = getData.retention_protection_increase;
        target.Ative = false;
        target.ChoiceItem = false;
    }

    void SetItem(TowerItem target, RepairManData getData)
    {
        target.id = getData.id;
        target.effect1 = getData.hpX;
        target.effect2 = getData.healingX;
        target.increase1 = getData.hpX_increase;
        target.increase2 = getData.healingX_increase;
        target.retention_effect1 = getData.retention_hp;
        target.retention_increase1 = getData.retention_hp_increase;
        target.retention_effect2 = getData.retention_healing;
        target.retention_increase2 = getData.retention_healing_increase;
        target.Ative = false;
        target.ChoiceItem = false;
    }
}
