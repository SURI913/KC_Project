using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    // Start is called before the first frame update
    [NonSerialized] public double Cost;
    [NonSerialized] public int index_item;

    [NonSerialized] public InventorySlot[] child;
    // * TODO
    // 무기 아이템 겹치는 경우 vs 포트리스사가처럼 따로 두는 경우 나눠서할 경우 확인 필요(컨펌: 채연씨)



    private void Awake()
    {
        child = GetComponentsInChildren<InventorySlot>(); //자식들 다 가져옴


        //데이터 생성

    }

    //항목별 사용 여부 확인
    public bool PerUsingItem(int _index )
    {
        if (child[_index].item.amount <= 0) return false;
        else return true;
    }

    //아이템 사용
    public bool UseItem (int _index, double _amount)
    {
        bool result =  child[_index].item.Use(_amount);
        child[_index].OnUIUpdate();
        return result;
    }

    //아이템 값 추가
    public bool AddItem(int _index, double _amount)
    {
        bool result = child[_index].item.SetAmount(_amount);
        child[_index].OnUIUpdate();
        return result;
    }
}
