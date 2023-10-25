using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class StageClear : MonoBehaviour
{
    public GameObject stageClearText;  // Stage Clear �ؽ�Ʈ

    public void OnBossClear()  // ������ ����Ʈ���� ȣ���� �Լ�
    {
        stageClearText.SetActive(true);  // Stage Clear �ؽ�Ʈ�� Ȱ��ȭ
        StartCoroutine(LoadNextScene());  // ���� Scene �ε� �ڷ�ƾ ȣ��
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2f);  // 2�� ��� (Stage Clear ǥ�� �ð�)

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  // ���� Scene �ε��� ��������
        int nextSceneIndex = currentSceneIndex + 1;  // ���� Scene �ε���

        // ������ Scene�̸� ���� ���� �Ǵ� ���� �޴��� ���ư�(�ɼ�)
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            // Application.Quit();  // ���� ���� (�����⿡���� �۵����� ����) �Ǵ�
            // SceneManager.LoadScene(0);  // ���� �޴��� ���ư��� (���� �޴��� ù ��° Scene�� ���)
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);  // ���� Scene �ε�
        }
    }
}
