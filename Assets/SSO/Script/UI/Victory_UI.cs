using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Victory_UI : MonoBehaviour
{
    public Boss boss;
    public TextMeshProUGUI gold;
    [SerializeField]
    private Enemy_Respown enemyRespawner;  // 참조

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.hp <= 0)
        {
            gameObject.SetActive(true);
        }

        gold.text = enemyRespawner.gold.ToString();
    }
}
