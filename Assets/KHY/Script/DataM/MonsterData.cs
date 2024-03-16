using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

[System.Serializable]
public class MonsterD
{
    public string dungeon_monster_stageID;
    public double dungeon_monster_hp;
    public double dungeon_monster_attack;
    public int dungeon_monster_atktime;
    public double dungeon_monster_recommattack;
    public double dungeon_monster_recommdefense;   
}


[CreateAssetMenu(fileName ="Monster Data",menuName ="Scriptable Object/Monster")]
public class MonsterData : ScriptableObject
{
    public MonsterD[] dungeon_monsterdatas;
}
