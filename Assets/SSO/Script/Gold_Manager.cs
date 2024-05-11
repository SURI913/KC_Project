using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gold_Manager : MonoBehaviour
{
    private TextMeshProUGUI goldText;

    private void Start()
    {
        goldText = GetComponent<TextMeshProUGUI>();
        if (goldText == null)
        {
            Debug.LogError("GoldDisplay script must be attached to a TextMeshProUGUI object!");
        }
    }

    private void Update()
    {
        if (goldText != null && Enemy_Respown.Instance != null)
        {
            goldText.text = "Gold: " + Enemy_Respown.Instance.gold.ToString();
        }
    }
}
