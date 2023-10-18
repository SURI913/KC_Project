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
    public double hpLv { get; private set; }
    public double hpMaxLv { get; private set; }
    public double attackLv { get; private set; }
    public double attackMaxLv { get; private set; }
    private int indexHp;
    private int indexAttack;

    [SerializeField] GrowthData growthData;

    private void Awake()
    {
        indexHp = 0;
        GetHpData();
        indexAttack = 0;
        GetAttackData();
    }

    void GetHpData()
    {
        HpData[] hpDataSet = growthData.GrowthHp;
        hpLv = hpDataSet[indexHp].Lv;
        hpMaxLv = hpDataSet[indexHp].MaxLV;
        hp = hpDataSet[indexHp].Hp;
        hpIncrease = hpDataSet[indexHp].Increase;
    }

    void GetAttackData()
    {
        AttackData[] attackDataSet = growthData.GrowthAttack;
        attackLv = attackDataSet[0].Lv;
        attackMaxLv = attackDataSet[0].MaxLV;
        attack = attackDataSet[0].Attack;
        hpIncrease = attackDataSet[0].Increase;
    }
    public void attackGrowUp()
    {
        if(attackLv < attackMaxLv)
        {
            //성장가능
            attack += attackIncrease;
            attackLv++;
        }
        else
        {
            if(indexAttack < growthData.GrowthAttack.Length)
            {
                indexAttack++;
                GetAttackData();
            }
        }
    }

    public void hpGrowUp()
    {
        if (hpLv < hpMaxLv)
        {
            //성장가능
            hp += hpIncrease;
            hpLv++;
        }
        else
        {
            if (indexHp < growthData.GrowthHp.Length)
            {
                indexHp++;
                GetHpData();
            }
        }
    }
}
