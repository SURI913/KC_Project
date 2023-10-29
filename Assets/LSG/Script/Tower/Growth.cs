using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
public class Growth : MonoBehaviour
{
    public double attack {  get; private set; }
    public double attackIncrease {  get; private set; }
    public double hp {  get; private set; }
    public double hpIncrease {  get; private set; }
    public int hpLv { get; private set; }
    public int hpMaxLv { get; private set; }
    public int attackLv { get; private set; }
    public int attackMaxLv { get; private set; }
    private int indexHp;
    private int indexAttack;

    [SerializeField] GrowthData growthGetData;
    [SerializeField] GrowingData growingSetData;

    private void Awake()
    {
        indexHp = 0;
        GetHpData();
        indexAttack = 0;
        GetAttackData();
    }

    void GetHpData()
    {
        HpData[] hpDataSet = growthGetData.GrowthHp;
        hpLv = hpDataSet[indexHp].Lv;
        hpMaxLv = hpDataSet[indexHp].MaxLV;
        hp = hpDataSet[indexHp].Hp;
        hpIncrease = hpDataSet[indexHp].Increase;

        growingSetData.Hp = hp;
        
    }

    void GetAttackData()
    {
        AttackData[] attackDataSet = growthGetData.GrowthAttack;
        attackLv = attackDataSet[0].Lv;
        attackMaxLv = attackDataSet[0].MaxLV;
        attack = attackDataSet[0].Attack;
        hpIncrease = attackDataSet[0].Increase;

        growingSetData.Attack = attack;
    }
    public void attackGrowUp()
    {
        if(attackLv <= attackMaxLv)
        {
            //성장가능
            attack += attackIncrease;
            attackLv++;
            growingSetData.Attack = attack + attackIncrease; //성장된 데이터 값 보내 줄 용도

        }
        else
        {
            if(indexAttack < growthGetData.GrowthAttack.Length)
            {
                indexAttack++;
                GetAttackData();
            }
        }
    }

    public void hpGrowUp()
    {
        if (hpLv <= hpMaxLv) 
        {
            //성장가능
            hp += hpIncrease;
            hpLv++;
            growingSetData.Hp = hp + hpIncrease;
        }
        else
        {
            if (indexHp < growthGetData.GrowthHp.Length)
            {
                indexHp++;
                GetHpData();
            }
        }
    }
}
