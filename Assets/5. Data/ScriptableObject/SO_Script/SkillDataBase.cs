using Default;
using UnityEngine;

[System.Serializable]
public class CharacterSkillData: IData<string>
{
    [SerializeField] public string character_id;
    [SerializeField] public string name;
    [SerializeField] public Sprite skill_sprite;
    [SerializeField] [TextArea] public string skill_comment;

    public string GetKey() => character_id;
}

[CreateAssetMenu(fileName = "AllSkill", menuName = "Character Data / Skill Data")]
public class SkillDataBase : GenericDatabase<string, CharacterSkillData>
{

}


