using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyItemData", menuName = "Scriptable Objest/ItemData/CurrencyItemData")]
public class CurrencyItemData : ItemData
{
    /// <summary> 재화아이템 </summary>
    public double amount { get; protected set; }
    [SerializeField] private double _amount;

    /// <summary> 개수 지정(범위 제한) </summary>
    public void SetAmount(double _amount)
    {
        amount += _amount;
    }

    public bool Use(double _amount)
    {
        if ((amount - _amount) >= 0)
        {
            amount -= _amount;
            return true;
        }

        return false;
    }

    public override Item CreateItem()
    {
        return new CurrencyItem(this);
    }
}