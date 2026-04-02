using System;
using DG.Tweening;
using UnityEngine;


public class FollowSpine : MonoBehaviour
{
    public Transform[] points; 
    public float duration = 5f;

    void Start()
    {
        Vector3[] waypoints = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
            waypoints[i] = points[i].position;

        transform.DOPath(waypoints, duration, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetOptions(false); 
    }
}