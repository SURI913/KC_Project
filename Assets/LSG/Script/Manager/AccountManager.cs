using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;

public class AccountManager : MonoBehaviour
{
    public TMP_InputField username_input;
    public TMP_InputField email_input;
    public TMP_InputField password_input;

    

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


    void OnLoginSuccess(LoginResult result)
    {
        print("로그인 성공");

        GameManager.instance.GetUserData(result.PlayFabId);
        GameManager.instance.ClientGetTitleData();
        SceneManager.LoadScene("Main");
    }

    void OnLoginFailure(PlayFabError error) => print("로그인 실패");

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        print("회원가입 성공");
        GameManager.instance.InitUserData();
        GameManager.instance.GetUserData(result.PlayFabId);
        SceneManager.LoadScene("Main");
    }

    void OnRegisterFailure(PlayFabError error) => print("회원가입 실패");

}
