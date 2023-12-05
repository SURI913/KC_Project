using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

[System.Serializable]
public class MonsterD
{
    public string stageID;
    public double hp;
    public double attack;
    public int atktime;
    public double recommattack;
    public double recommdefense;   
}


[CreateAssetMenu(fileName ="Monster Data",menuName ="Scriptable Object/Monster")]
public class MonsterData : ScriptableObject
{
    public MonsterD[] monsterdatas;
}
