using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CurrentTowerData", menuName = "Scriptable Objest/CurrentTowerData")]
public class CurrentTowerData : ScriptableObject
{
    //타워 배수(착용효과)
    public double attackX;
    public double protectionX;
    public double hpX;
    public double healingX;

    //타워 보유효과
    public double retention_attack;
    public double retention_protection;
    public double retention_hp;
    public double retention_healing;
}


