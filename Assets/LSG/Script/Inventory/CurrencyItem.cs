using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyItem : Item, UsableItemImp
{
    public CurrencyItemData CurrentItemData { get; private set; }

    /// <summary> 현재 아이템 개수 </summary>
    public double Amount { get; protected set; }

    /// <summary> 개수 지정(범위 제한) </summary>
    public void SetAmount(double _amount)
    {
        Amount += _amount;
    }

    public bool Use(double _amount)
    {
        if ((Amount - _amount) >= 0)
        {
            Amount -= _amount;
            return true;
        }

        return false;
    }

    public  CurrencyItem(CurrencyItemData data, double amount = 0) : base(data)  //생성자
    {
        CurrentItemData = data;
        stackable = true;
        SetAmount(amount);
    }

    public override Item CreateItem()
    {
        return new CurrencyItem(CurrentItemData, Amount);
    }
}
