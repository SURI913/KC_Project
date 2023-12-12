using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

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

        btn = GetComponentsInChildren<Button>();
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
        atkativelevelup = false;

        if (attackLv <= attackMaxLv)
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
        hpativelevelup = false;

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

    //일시적으로 제한
    private float atkcooltime = 10f;
    private bool atkativelevelup = true;
    private Button[] btn;

    private float hpcooltime = 10f;
    private bool hpativelevelup = true;

    private void Update()
    {
        if (atkcooltime >= 0 && !atkativelevelup)
        {
            atkcooltime -= Time.deltaTime;
            btn[0].interactable = false;

        }
        else
        {
            atkativelevelup = true;
            atkcooltime = 10f;
            btn[0].interactable = true;
        }

        if (hpcooltime >= 0 && !hpativelevelup)
        {
            hpcooltime -= Time.deltaTime;
            btn[1].interactable = false;

        }
        else
        {
            hpativelevelup = true;
            hpcooltime = 10f;
            btn[1].interactable = true;
        }
    }
}
