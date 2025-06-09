using Default;
using UnityEngine;


[System.Serializable]
public class CharacterUISkillDescription : IData<string>
{
    [SerializeField] public string _character_id;
    [SerializeField] public string _name;
    [SerializeField] public Sprite _skill_sprite;
    [SerializeField] [TextArea] public string _skill_description;

    public string GetKey() => _character_id;
}

[CreateAssetMenu(fileName = "UISkillDescriptions", menuName = "Character Data / Skill Description")]
public class SkillDataBase : GenericDatabase<string, CharacterUISkillDescription>
{

}


