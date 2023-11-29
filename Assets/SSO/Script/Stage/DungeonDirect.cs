using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonDirect : MonoBehaviour
{
    private Button sceneButton;
    private void Start()
    {
        sceneButton = GetComponent<Button>();
        sceneButton.onClick.AddListener(StageSelectionSceneLode);
    }

    public void StageSelectionSceneLode()
    {
        SceneManager.LoadScene("Dungeon");
    }
}
