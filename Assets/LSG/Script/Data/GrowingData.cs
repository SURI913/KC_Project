using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrowingData", menuName = "Scriptable Objest/GrowingData")]
public class GrowingData : ScriptableObject
{
    public double Hp;
    public double Attack;
    public double protection;
    public double healing;
}
