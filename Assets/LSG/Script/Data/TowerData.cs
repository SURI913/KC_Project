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
    public double retention_attack;
    public double retention_protection;
    public double retention_increase_protection;
    public double retention_increase_attack;
    public double attackX;
    public double protectionX;
    public double attackX_increase;
    public double protectionX_increase;
    //public Sprite IMG = null;
    //public Image UI_IMG = null;

}

[System.Serializable]
public class RepairManData
{
    public string ID;
    public string Tier;
    public double retention_hp;
    public double retention_increase;
    public double retention_healing;
    public double retention_increase_healing;
    public double hpX;
    public double hpX_increase;
    public double healingX;
    public double healingX_increase;
    //public Sprite IMG = null;
    //public Image UI_IMG = null;
}
