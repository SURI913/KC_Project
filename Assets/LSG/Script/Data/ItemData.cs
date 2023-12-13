using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class ItemData : ScriptableObject //아이템 공통 데이터
{
    [SerializeField] public int id;
    [SerializeField] public new string name;

    [Multiline]
    [SerializeField] public string description;
    [SerializeField] public Sprite icon_sprite;

    /// <summary> 타입에 맞는 새로운 아이템 생성 </summary>
    public abstract Item CreateItem();
}

public abstract class Item : ItemData
{
    public ItemData Data { get; private set; }
    public Item(ItemData data) => Data = data; //값저장 람다함수

    protected bool stackable; //쌓일 수 있는 데이터인가
}