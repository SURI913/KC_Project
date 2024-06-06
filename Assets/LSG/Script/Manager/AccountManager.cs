using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class AccountManager : MonoBehaviour
{
    public TMP_InputField username_input;
    public TMP_InputField email_input;
    public TMP_InputField password_input;

    static string playfabID;

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = email_input.text, Password = password_input.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }


    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Username = username_input.text, Email = email_input.text, Password = password_input.text, RequireBothUsernameAndEmail = true };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }


    void OnLoginSuccess(LoginResult result) => print("로그인 성공");

    void OnLoginFailure(PlayFabError error) => print("로그인 실패");

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        playfabID = result.PlayFabId;
        print("회원가입 성공");
    }

    void OnRegisterFailure(PlayFabError error) => print("회원가입 실패");
}
