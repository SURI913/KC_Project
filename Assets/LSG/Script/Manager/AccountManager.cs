using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class AccountManager : MonoBehaviour
{
    public TMP_InputField username_input;
    public TMP_InputField email_input;
    public TMP_InputField password_input;

    static string customId = "";
    static string playfabId = "";

    private string entityId;
    private string entityType;

    public void OnClickGuestLogin() //게스트 로그인 버튼
    {
        if (string.IsNullOrEmpty(customId))
            CreateGuestId();
        else
            LoginGuestId();
    }

    private void CreateGuestId() //저장된 아이디가 없을 경우 새로 생성
    {
        customId = GetRandomPassword(16);

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CustomId = customId,
            CreateAccount = true
        }, result =>
        {
            OnLoginSuccess(result);
        }, error =>
        {
            Debug.LogError("Login Fail - Guest");
        });
    }
    private string GetRandomPassword(int _totLen) //랜덤한 16자리 id 생성
    {
        string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var chars = Enumerable.Range(0, _totLen)
            .Select(x => input[UnityEngine.Random.Range(0, input.Length)]);
        return new string(chars.ToArray());
    }

    private void LoginGuestId() //게스트 로그인
    {
        Debug.Log("Guest Login");

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CustomId = customId,
            CreateAccount = false
        }, result =>
        {
            OnLoginSuccess(result);
        }, error =>
        {
            Debug.LogError("Login Fail - Guest");
        });
    }

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
