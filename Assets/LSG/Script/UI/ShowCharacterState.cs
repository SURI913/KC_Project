using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharacterState : MonoBehaviour
{
    BaseCatData[] my_state_show_data; 
    public TMP_Text[] my_text;
    public Image[] my_image;
    public Image[] my_gauge_color;

    void Start()
    {
        ChangeCat();
    }

    public void ChangeCat()
    {
        GameObject[] my_cat;
        my_cat = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(my_cat.Length);
        for (int i = 0; i < my_cat.Length; i++)
        {
            my_state_show_data[i] = my_cat[i].GetComponent<Cat>().cat_data;
            my_text[i].text = my_state_show_data[i]._name;
            my_image[i].sprite = my_state_show_data[i]._sprite;
            my_gauge_color[i].color = my_state_show_data[i]._main_color;
        }

    }

    //이후 캐릭터 선택창 만들어지면 수정하는걸로
    private void Update()
    {
        
    }

}
