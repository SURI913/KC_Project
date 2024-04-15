using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    [System.Serializable]
    private class SkillIMGInfo
    {
        public string UIName;

        public Sprite skill_img;

    }

    [SerializeField]
    private SkillIMGInfo[] skill_info = null;

    // Start is called before the first frame update
    private SkillUserImp my_skills;

    
    void Start()
    {
        my_skills = GameObject.FindWithTag("Player").GetComponent<SkillUserImp>();

            for (int idx = 0; idx < skill_info.Length; idx++)
            {
            transform.GetChild(idx).GetComponentInChildren<Image>().sprite = skill_info[idx].skill_img;
            //transform.GetChild(idx).GetComponent<Button>().onClick.AddListener(StartCoroutine(my_skills.OnSkill(my_skills)));
        }
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
