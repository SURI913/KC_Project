using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DistanceComparer : IComparer
{
    //값을 받아와서
    private Transform target_transform;

    public DistanceComparer(Transform target_transform)
    {
        this.target_transform = target_transform;
    }

    public int Compare(object x, object y)
    {
        Collider x_collider =  x as Collider;
        Collider y_collider =  y as Collider;

        if (target_transform == null) return -1; //==>나중에 처리
        Vector3 offset =  x_collider.transform.position - target_transform.position;
        float x_distance = offset.sqrMagnitude;

        offset = y_collider.transform.position - target_transform.position;
        float y_distance = offset.sqrMagnitude;

        return x_distance.CompareTo(y_distance);
    }
}
