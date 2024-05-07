using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Objest/Skill Data")]
public class SkillData : ScriptableObject
{
    // 오브젝트 이름
    public string parent_name;
    // 오브젝트 풀에서 관리할 오브젝트
    public GameObject perfab;

    public float cool_time;

    public Sprite img;
}
