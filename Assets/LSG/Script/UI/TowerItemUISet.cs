using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerItemUISet : MonoBehaviour
{
    public TowerData towerData;

    //아이템 배치
    public GameObject cannonScollBar;
    public GameObject repairmanScollBar;

    public CannonData[] FindcannonData;
    public RepairManData[] FindrepairmanData;

    TowerItem[] Setcannon;
    TowerItem[] Setrepairman;


    private void Awake()
    {
        if (towerData != null)
        {
            // 데이터 저장할 곳 가져오기
            Setcannon = cannonScollBar.GetComponentsInChildren<TowerItem>();
            Setrepairman = repairmanScollBar.GetComponentsInChildren<TowerItem>();
            
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
        }
    }

    void SetItem(TowerItem target, CannonData getData)
    {
        target.ID = getData.ID;
        target.effect = getData.Attack;
        target.increase = getData.Increase;
        target.RetentionEffect = getData.RetentionAttack;
        target.RetentionIncrease = getData.RetentionIncrease;
        target.Ative = false;
    }

    void SetItem(TowerItem target, RepairManData getData)
    {
        target.ID = getData.ID;
        target.effect = getData.Hp;
        target.increase = getData.Increase;
        target.RetentionEffect = getData.RetentionHp;
        target.RetentionIncrease = getData.RetentionIncrease;
        target.Ative = false;


    }
}
