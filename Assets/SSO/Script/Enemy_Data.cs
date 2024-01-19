
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public string name;
    public double hp;
    public float damage;
    public float enemySpeed;
}

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data")]
public class Enemy_Data : ScriptableObject
{
    public Enemy[] enemys;
}
