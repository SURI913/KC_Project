using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeUserData : MonoBehaviour
{
    // Start is called before the first frame update

    TMP_Text lv;
    TMP_Text display_name;
    void Start()
    {
        lv = transform.GetChild(2).GetComponent<TMP_Text>();
        display_name = transform.GetChild(3).GetComponent<TMP_Text>();
        lv.text = "Lv." + GameManager.instance.lv;
        display_name.text = GameManager.instance.player_display_name;
    }
}
