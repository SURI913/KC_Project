using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InventoryObject", menuName = "Scriptable Objest/MyInventoryObject")]
public abstract class ItemData : ScriptableObject //아이템 공통 데이터
{
    [SerializeField] private int id;
    [SerializeField] private string name;

    [Multiline]
    [SerializeField] private string description;
    [SerializeField] private Sprite icon_sprite;

    /// <summary> 타입에 맞는 새로운 아이템 생성 </summary>
    public abstract Item CreateItem();
}

public abstract class Item : ItemData
{
    public ItemData Data { get; private set; }
    public Item(ItemData data) => Data = data; //값저장 람다함수

    bool stackable; //쌓일 수 있는 데이터인가
}