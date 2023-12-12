using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrowthData", menuName = "Scriptable Objest/Data")]
public class GrowthData : ScriptableObject
{
    public AttackData[] GrowthAttack;
    public HpData[] GrowthHp;
    public ProtectionData[] growth_protection;
    public HealingData[] growth_heal;
}

[System.Serializable]
public class AttackData
{
    public int Lv;
    public int MaxLV;
    public double Attack;
    public double Increase;
    public double sub_curreny_min;
    public double sub_curreny_max;
    public double sub_curreny_Increase;
}

[System.Serializable]
public class HpData
{
    public int Lv;
    public int MaxLV;
    public double Hp;
    public double Increase;
    public double sub_curreny_min;
    public double sub_curreny_max;
    public double sub_curreny_Increase;
}

[System.Serializable]
public class ProtectionData
{
    public int Lv;
    public int MaxLV;
    public double protection;
    public double Increase;
    public double sub_curreny_min;
    public double sub_curreny_max;
    public double sub_curreny_Increase;
}

[System.Serializable]
public class HealingData
{
    public int Lv;
    public int MaxLV;
    public double healing;
    public double Increase;
    public double sub_curreny_min;
    public double sub_curreny_max;
    public double sub_curreny_Increase;
}
