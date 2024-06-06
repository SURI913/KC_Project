using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "Scriptable Objest/UserData")]
[System.Serializable]

public class UserData : ScriptableObject
{
    public int _lv;
    public float _ex;
    public string _current_stage;


}

