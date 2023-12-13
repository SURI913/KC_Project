using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AllUnit;


public class Growth : MonoBehaviour
{
    public double attack {  get; private set; }
    public double attackIncrease {  get; private set; }
    public double hp {  get; private set; }
    public double hpIncrease {  get; private set; }
    public double protection { get; private set; }
    public double protection_increase { get; private set; }
    public double healing { get; private set; }
    public double healing_increase { get; private set; }

    public int hpLv { get; private set; }
    public int hpMaxLv { get; private set; }
    public int attackLv { get; private set; }
    public int attackMaxLv { get; private set; }
    public int protection_lv { get; private set; }
    public int protection_max_lv { get; private set; }
    public int healing_lv { get; private set; }
    public int healing_max_lv { get; private set; }

    private int indexHp;
    private int indexAttack;
    private int index_protection;
    private int index_healing;

    [SerializeField] GrowthData growthGetData;
    [SerializeField] GrowingData growingSetData;

    private void Awake()
    {
        indexHp = 0;
        GetHpData();
        indexAttack = 0;
        GetAttackData();
        index_protection = 0;
        GetProtectionData();
        index_healing = 0;
        GetHealingData();

        SetUIInIt();

        btn = GetComponentsInChildren<Button>();

        //--- ui 세팅
        sub_curreny_attack_text.text = Unit.ToUnitString(curreny_attack);
        curreny_attack_text.text = Unit.ToUnitString(attack);
        update_attack_text.text = Unit.ToUnitString(attack+attackIncrease);

        sub_curreny_hp_text.text = Unit.ToUnitString(curreny_hp);
        curreny_hp_text.text = Unit.ToUnitString(hp);
        update_hp_text.text = Unit.ToUnitString(hp + hpIncrease);

        sub_curreny_protection_text.text = Unit.ToUnitString(curreny_protection);
        curreny_protection_text.text = Unit.ToUnitString(protection);
        update_protection_text.text = Unit.ToUnitString(protection + protection_increase);

        sub_curreny_healing_text.text = Unit.ToUnitString(curreny_healing);
        curreny_healing_text.text = Unit.ToUnitString(healing);
        update_healing_text.text = Unit.ToUnitString(healing + healing_increase);
    }

    private double curreny_attack;
    private double curreny_hp;
    private double curreny_protection;
    private double curreny_healing;

    TextMeshProUGUI sub_curreny_attack_text;
    TextMeshProUGUI curreny_attack_text;
    TextMeshProUGUI update_attack_text;
    TextMeshProUGUI sub_curreny_hp_text;
    TextMeshProUGUI curreny_hp_text;
    TextMeshProUGUI update_hp_text;
    TextMeshProUGUI sub_curreny_protection_text;
    TextMeshProUGUI curreny_protection_text;
    TextMeshProUGUI update_protection_text;
    TextMeshProUGUI sub_curreny_healing_text;
    TextMeshProUGUI curreny_healing_text;
    TextMeshProUGUI update_healing_text;
    void SetUIInIt()
    {
        sub_curreny_attack_text = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        curreny_attack_text = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        update_attack_text = transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();


        sub_curreny_hp_text = transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        curreny_hp_text = transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        update_hp_text = transform.GetChild(1).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();


        sub_curreny_protection_text = transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        curreny_protection_text = transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        update_protection_text = transform.GetChild(2).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();

        sub_curreny_healing_text = transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        curreny_healing_text = transform.GetChild(3).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        update_healing_text = transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();

    }

    void GetHpData()
    {
        HpData[] hpDataSet = growthGetData.GrowthHp;
        hpLv = hpDataSet[indexHp].Lv;
        hpMaxLv = hpDataSet[indexHp].MaxLV;
        hp = hpDataSet[indexHp].Hp;
        hpIncrease = hpDataSet[indexHp].Increase;
        curreny_hp = hpDataSet[indexHp].sub_curreny_min;
        growingSetData.Hp = hp;

    }

    void GetProtectionData()
    {
        ProtectionData[] protectionDataSet = growthGetData.growth_protection;
        protection_lv = protectionDataSet[index_protection].Lv;
        protection_max_lv = protectionDataSet[index_protection].MaxLV;
        protection = protectionDataSet[index_protection].protection;
        protection_increase = protectionDataSet[index_protection].Increase;
        curreny_protection = protectionDataSet[index_protection].sub_curreny_min;

        growingSetData.protection = protection;
        
    }
    void GetHealingData()
    {
        HealingData[] healingDataSet = growthGetData.growth_heal;
        healing_lv = healingDataSet[index_healing].Lv;
        healing_max_lv = healingDataSet[index_healing].MaxLV;
        healing = healingDataSet[index_healing].healing;
        healing_increase = healingDataSet[index_healing].Increase;
        curreny_healing = healingDataSet[index_healing].sub_curreny_min;

        growingSetData.healing = healing;

    }

    void GetAttackData()
    {
        AttackData[] attackDataSet = growthGetData.GrowthAttack;
        attackLv = attackDataSet[indexAttack].Lv;
        attackMaxLv = attackDataSet[indexAttack].MaxLV;
        attack = attackDataSet[indexAttack].Attack;
        attackIncrease = attackDataSet[indexAttack].Increase;
        curreny_attack = attackDataSet[indexAttack].sub_curreny_min;

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
            growingSetData.Attack = attack; //성장된 데이터 값 보내 줄 용도

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
            growingSetData.Hp = hp;
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

    public void ProtectionGrowUp()
    {
        protection_ativelevelup = false;

        if (protection_lv <= protection_max_lv)
        {
            //성장가능
            protection += protection_increase;
            protection_lv++;
            growingSetData.protection = protection;
        }
        else
        {
            if (indexHp < growthGetData.growth_protection.Length)
            {
                indexHp++;
                GetProtectionData();
            }
        }
    }

    public void HealingGrowUp()
    {
        healing_ativelevelup = false;

        if (healing_lv <= healing_max_lv)
        {
            //성장가능
            healing += healing_increase;
            healing_lv++;
            growingSetData.healing = healing;
        }
        else
        {
            if (index_healing < growthGetData.growth_heal.Length)
            {
                index_healing++;
                GetHealingData();
            }
        }
    }

    //일시적으로 제한
    private float atkcooltime = 10f;
    private bool atkativelevelup = true;
    private Button[] btn;

    private float hpcooltime = 10f;
    private bool hpativelevelup = true;

    private float protection_cooltime = 10f;
    private bool protection_ativelevelup = true;

    private float healing_cooltime = 10f;
    private bool healing_ativelevelup = true;

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
        if (protection_cooltime >= 0 && !protection_ativelevelup)
        {
            protection_cooltime -= Time.deltaTime;
            btn[2].interactable = false;

        }
        else
        {
            protection_ativelevelup = true;
            protection_cooltime = 10f;
            btn[2].interactable = true;
        }
        if (healing_cooltime >= 0 && !healing_ativelevelup)
        {
            healing_cooltime -= Time.deltaTime;
            btn[3].interactable = false;

        }
        else
        {
            healing_ativelevelup = true;
            healing_cooltime = 10f;
            btn[3].interactable = true;
        }
    }
}
