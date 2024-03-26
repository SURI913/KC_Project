using System;
using UnityEngine;

public class LookTarget : MonoBehaviour
{
    /// <summary>
    /// 타겟 감지하는 스크립트 게임이 시작하고 캐릭터들이 생성되었을 때? 돌아가게?
    /// </summary>

    public Vector2 check_size;
    public LayerMask checkLayers;

    const float time_set = 5f;
    public float cooltime = time_set;

    public Collider2D[] colliders { get; set; } // 가변배열

    private void Update()
    {

        colliders = Physics2D.OverlapBoxAll(transform.position, check_size, 0f, checkLayers);
        Array.Sort(colliders, new DistanceComparer(transform));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, check_size);
    }
}
