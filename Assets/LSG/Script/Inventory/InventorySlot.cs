using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ Serializable] //직렬화
public class InventorySlot : MonoBehaviour
{
    //인벤토리 아이템 불러옴
    //[NonSerialized] public ItemObject parent;
    [NonSerialized] public GameObject slotUI;

     /*public ItemObject InventoryItem
    {
        get
        {
            return item.name != null ? parent.database.ItemObject.id : null;
        }
    }*/

    //슬롯이 업데이트 될 떄 부가적인 처리가 가능하도록 하는 이벤트
    [NonSerialized] public Action<InventorySlot> OnPreUpdate; 
    [NonSerialized] public Action<InventorySlot> OnPostUpdate;

    public Item item;
    public int amout;

}
