using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Currency_ItemData", menuName = "Scriptable Objest/CurrencyItemData")]
public class CurrencyItemData
{
    /// <summary> 재화아이템 </summary>
   /* public double amount => amount;
    [SerializeField] private double _amount;
    public override Item CreateItem()
    {
        return new CurrencyItem(this);
    }*/
}

/*public abstract class CurrencyItem : Item
{
    public CurrencyItemData CurrentItem { get; private set; }
    public CurrencyItem(CurrencyItemData data, int amount = 1) : base(data, amount) { }

    public bool Use( double _amount)
    {
        // 임시 : 개수 하나 감소
        //amount -= _amount;

        return true;
    }

    protected override CurrencyItem Clone(int amount)
    {
       // return new CurrencyItem( as CurrencyItemData, amount);
    }

}*/