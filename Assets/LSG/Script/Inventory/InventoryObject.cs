using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    // Start is called before the first frame update
    public double Cost;
    public int index_item;

    [NonSerialized] public InventorySlot[] child;



    private void Awake()
    {
        child = GetComponentsInChildren<InventorySlot>(); //자식들 다 가져옴
        int i = 0;

        //데이터 생성

    }

    //구매 전 금액 체크
    public double PerUsingItem(int _index )
    {
        return child[_index].item.amount;
    }

    public bool PurchaseItem (int _index, double _amount)
    {
        bool result =  child[_index].item.Use(_amount);
        child[_index].OnUIUpdate();
        return result;
    }


}
