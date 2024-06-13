using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Cat Data", menuName = "Scriptable Objest/Cat Data")]

public class CatData : ScriptableObject
{
    public List<BaseCatData> all_cat_data;
}

[System.Serializable]
public class BaseCatData
{
    public string _id;
    public string _name;
    public string _tier;
    public float _hp_multipler;
    public float _increase_hp;
    public float _attack_multipler;
    public float _increase_attack;
    public float _protection_multiple;
    public float _increase_protect;
    public float _healing_multiple;
    public float _increase_healing;
    public float _critical_hit_average;
    public float _critical_hit_attack;
    public float _attack_speed;
    public float _atk_time;
    public float _skl_time;
    public float _skl_effect;
}
