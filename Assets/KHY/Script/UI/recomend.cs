using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class recomend : Monster
{
   
    // Start is called before the first frame update
    public Text reco;
   // Monster _monster = GameObject.FindWithTag("Enemy").GetComponent<Monster>();

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

       // reco.text = "Recomend Attack: " + _monster.recomend_attack.ToString() + "\n" +
                           // "Recomend Defense: " + _monster.recomend_defense.ToString();
    }
}
