using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrowthData", menuName = "Scriptable Objest/Data")]
public class GrowthData : ScriptableObject
{
    public AttackData[] GrowthAttack;
    public HpData[] GrowthHp;
}

[System.Serializable]
public class AttackData
{
    public int Lv;
    public int MaxLV;
    public double Attack;
    public double Increase;
}

[System.Serializable]
public class HpData
{
    public int Lv;
    public int MaxLV;
    public double Hp;
    public double Increase;
}
