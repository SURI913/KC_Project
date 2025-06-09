using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Default;
using PlayFab.GroupsModels;

public class UIManager : MonoBehaviour
{
    public GameObject skillButtonParent;
    private Button[] skillButtons = new Button[Constants.ACTIVECHARACTER_COUNT];
    private Image[] skillButtonSprite = new Image[Constants.ACTIVECHARACTER_COUNT];
    void Awake()
    {
        skillButtons = skillButtonParent.GetComponentsInChildren<Button>();
        skillButtonSprite = skillButtonParent.GetComponentsInChildren<Image>();
    }

    public void RemoveSkillButtonSetting(int index)
    {
        //캐릭터 최대갯수가 넘지않을 때 호출해서 변경 됨
        skillButtons[index].onClick.RemoveAllListeners();
    }

    //게임매니저에서 시작 & 캐릭터 변경 할때 호출 (전체 변경)
    public void UpadatSkillButtonSetting(List<Cat> myCharacters)
    {
        //기존 연결 끊고 다시 연결
        foreach (var skillButton in skillButtons) skillButton.onClick.RemoveAllListeners();

        for(int i =0; i < Constants.ACTIVECHARACTER_COUNT; i++)
        {
            if (myCharacters[i] == null) return;
            skillButtons[i].onClick.AddListener(myCharacters[i].ActiveSkillAnimation);

        }
    }

    public void UpadatSkillButtonSetting(Cat myCharacter, int index)
    {
        if(skillButtons[index].onClick.GetPersistentEventCount() > 0)
        {
            RemoveSkillButtonSetting(index);
        }
        skillButtons[index].onClick.AddListener(myCharacter.ActiveSkillAnimation);
    }
}
