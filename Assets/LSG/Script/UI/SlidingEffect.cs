using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingEffect : MonoBehaviour
{
    bool open_state = false; // 처음 UI오픈 했다면
    Animator myAnim;
    private void Start()
    {
        myAnim = GetComponent<Animator>();

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    //------------------------------------------------------------------------캣타워 상세페이지 오픈/클로즈 컨트롤
    public void UIOpenButton()
    {
        if (!open_state)
        {
            myAnim.SetTrigger("isOpen");
            open_state= true;
        }
        
    }

    public void UICloseButton()
    {
        if (open_state)
        {
            myAnim.SetTrigger("isClose");
            open_state= false;
        }

    }

    //--------------------------------------------------------------------------------기본 성장 제어 버튼

    

}
