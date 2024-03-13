using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

[System.Serializable]
public class Enemy
{
    public string name;
    public double hp;
    public float damage;
}

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data")]
public class Enemy_Data : ScriptableObject
{
    public Enemy[] enemy1;
    public Enemy[] enemy2;
    public Enemy[] enemy3;
    public Enemy[] enemy4;
    public Enemy[] boss;
}
