using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecommendText : MonoBehaviour
{
    public Text recommendText;
    public StageButton ST;
    void Start()
    {
        //텍스트 비활성화
        recommendText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스가 버튼 위에 올라갔을 때
        if (IsMouseOverButton())
        {
            recommendText.gameObject.SetActive(true);
            recommendText.text = "마우스를 올리셨습니다!";
        }
        // 마우스가 버튼에서 벗어났을 때
        else
        {
            recommendText.gameObject.SetActive(false);
            recommendText.text = "";
        }
    }
    bool IsMouseOverButton()
    {
        // 마우스 포인터의 화면 좌표를 가져옴
        Vector2 mousePosition = Input.mousePosition;
        // 현재 버튼의 RectTransform을 가져옴
        RectTransform buttonRect = GetComponent<RectTransform>();

        // 마우스가 버튼 영역 안에 있는지 확인
        return RectTransformUtility.RectangleContainsScreenPoint(buttonRect, mousePosition);
    }
}
