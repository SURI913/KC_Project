using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ Serializable] //직렬화
public class InventorySlot : MonoBehaviour
{

    //인벤토리 아이템 불러옴
    [NonSerialized] InventoryObject parent; 
    [NonSerialized] public GameObject slotUI;

    // UI업데이트
    [NonSerialized] public Action  OnUIUpdate; 

    public CurrencyItemData item;

    //UI 오브젝트
    TextMeshProUGUI _amount_text;
    TextMeshProUGUI _content_text;
    Image _item_img;

    private void Awake()
    {
        parent = transform.parent.GetComponent<InventoryObject>();

        //UI세팅
        slotUI = gameObject;
        _amount_text = slotUI.GetComponentInChildren<TextMeshProUGUI>();
        _item_img = slotUI.GetComponentInChildren<Image>();

        OnUIUpdate += UpdateUI;

        //클릭 시 정보 보이도록
    }


    void UpdateUI()
    {
        _amount_text.text = item.amount.ToString();
        _item_img.sprite = item.icon_sprite;
    }

    void OnclickContent()
    {

    }
}
