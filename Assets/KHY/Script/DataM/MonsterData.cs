using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

[System.Serializable]
public class MonsterD
{
    public string D_stageID;
    public double D_hp;
    public double D_attack;
    public int D_atktime;
    public double D_recommattack;
    public double D_recommdefense;   
}


[CreateAssetMenu(fileName ="Monster Data",menuName ="Scriptable Object/Monster")]
public class MonsterData : ScriptableObject
{
    public MonsterD[] monsterdatas;
}
