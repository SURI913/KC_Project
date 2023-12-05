using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyItemData", menuName = "Scriptable Objest/ItemData/CurrencyItemData")]
public class CurrencyItemData : ItemData
{
    /// <summary> 재화아이템 </summary>
    public double amount => amount;
    [SerializeField] private double _amount;

    public override Item CreateItem()
    {
        return new CurrencyItem(this);
    }
}