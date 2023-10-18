using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptable Objest/Data")]
public class TowerData : ScriptableObject
{
    //��Ʈ �̸��̶� �����ϰ�
    public CannonData[] Cannon;
    public RepairManData[] RepairMan;
}

[System.Serializable]
public class CannonData
{
    public string ID;
    public string Tier;
    public double RetentionAttack;
    public double RetentionIncrease;
    public double Attack;
    public double Increase;
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
    public double Hp;
    public double Increase;
    //public Sprite IMG = null;
    //public Image UI_IMG = null;
}
