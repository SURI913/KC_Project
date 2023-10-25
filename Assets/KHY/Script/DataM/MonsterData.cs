using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterD
{

    public string stageID;
    public double hp;
    public double attack;
    public double atktime;
    public double recommattack;
    public double recommdefense;
}


[CreateAssetMenu(fileName ="Monster Data",menuName ="Scriptable Object/Monster")]
public class MonsterData : ScriptableObject
{
    public MonsterD[] monsterdatas;
   
    /*[SerializeField]
    private string stageID;
    public string StageID { get { return stageID; } }
    [SerializeField]
    private float hp;
    public float Hp { get { return hp; } }
    [SerializeField]
    private float attack;
    public float Attack { get { return attack; } }
    [SerializeField]
    private float atkTime;
    public float Atktime { get { return atkTime; } }
    [SerializeField]
    private float recommattack;
    public float Recommattack { get { return recommattack; } }
    [SerializeField]
    private float recommdefense;
    public float Recommdefense { get { return recommdefense; } }*/
}
