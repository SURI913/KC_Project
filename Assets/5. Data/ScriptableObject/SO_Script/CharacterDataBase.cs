using Default;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Data/Character Data")]
public class CharacterDataBase : ScriptableObject, IData<string>
{
    public string _character_id;
    public string _name;
    public double _max_hp;
    public float _attack_interval; //공격주기
    public string GetKey() => _character_id;

}

[CreateAssetMenu(fileName = "SelectedCatCharacter", menuName = "Character Data / Selected Cat Data")]
public class SelectedCharacterData: GenericDatabase<string, CharacterDataBase>
{
    public void Add(CharacterDataBase character)
    {
        if (items.Contains(character))
        {
            Debug.LogWarning("이미 선택된 캐릭터입니다.");
            return;
        }

        if (items.Count >= Constants.ACTIVECHARACTER_COUNT)
        {
            Debug.LogWarning("최대 선택 수 초과");
            return;
        }

        items.Add(character);
        if (dict == null) Initialize(); // null 체크는 항상
        if (!dict.ContainsKey(character.GetKey()))
        {
            dict.Add(character.GetKey(), character);
        }
    }
    
}
