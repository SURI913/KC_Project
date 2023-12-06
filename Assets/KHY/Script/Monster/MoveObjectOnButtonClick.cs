using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObjectOnButtonClick : MonoBehaviour
{
    public Transform targetTransform; // 이동할 목표 위치
    public float moveSpeed = 1.0f; // 이동 속도

    private void Start()
    {
        // 버튼에 이벤트 리스너 등록
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(MoveToObject);
        }
    }

    void MoveToObject()
    {
        // 현재 위치에서 목표 위치로 보간 이동
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * moveSpeed);
    }
}