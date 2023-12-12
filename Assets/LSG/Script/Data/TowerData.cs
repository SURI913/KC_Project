using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptable Objest/TowerData")]
public class TowerData : ScriptableObject
{
    //시트 이름이랑 동일하게
    public CannonData[] Cannon;
    public RepairManData[] RepairMan;
}

[System.Serializable]
public class CannonData
{
    public string ID;
    public string Tier;
    public double RetentionAttack;
    public double RetentionProtection;
    public double RetentionIncrease_Protection;
    public double RetentionIncrease_Attack;
    public double Attack;
    public double Protection;
    public double Attack_Increase;
    public double Protection_Increase;
    //public Sprite IMG = null;
    //public Image UI_IMG = null;

}

[System.Serializable]
public class RepairManData
{
    public string ID;
    public string Tier;
    public double RetentionHp;
    public double RetentionIncrease;
    public double RetentionHealing;
    public double RetentionIncrease_Healing;
    public double Hp;
    public double HpIncrease;
    public double Healing;
    public double HealingIncrease;
    //public Sprite IMG = null;
    //public Image UI_IMG = null;
}
