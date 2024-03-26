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
    public string name;
    public string id;
    public string tier;
    public double retention_attack;
    public double retention_attack_increase;
    public double retention_protection;
    public double retention_protection_increase;
    public double attackX;
    public double attackX_increase;
    public double protectionX;
    public double protectionX_increase;
    public float attack_cooltime;
    public float attack_cooltime_decrease;
    public float attack_speed;
    public float attack_speed_increase;
    public float skill_effect;
    public float skill_effect_increase;
    //public Sprite IMG = null;
    //public Image UI_IMG = null;

}

[System.Serializable]
public class RepairManData
{
    public string name;
    public string id;
    public string tier;
    public double retention_hp;
    public double retention_hp_increase;
    public double retention_healing;
    public double retention_healing_increase;
    public double hpX;
    public double hpX_increase;
    public double healingX;
    public double healingX_increase;
    public float attack_cooltime;
    public float attack_cooltime_decrease;
    public float attack_speed;
    public float attack_speed_increase;
    public float skill_effect;
    public float skill_effect_increase;
    //public Sprite IMG = null;
    //public Image UI_IMG = null;
}
