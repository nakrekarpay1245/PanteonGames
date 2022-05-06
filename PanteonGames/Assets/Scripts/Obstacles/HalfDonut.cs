using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonut : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Vector3[] points;

    [SerializeField]
    private GameObject stick;

    private Vector3 currentPoint;
    private int pointIndex;

    private void Start()
    {
        currentPoint = points[0];
    }

    void Update()
    {
        currentPoint = points[pointIndex % points.Length];

        if (Vector3.Distance(stick.transform.localPosition, currentPoint) < 0.05f)
        {
            pointIndex++;
        }

        stick.transform.localPosition = Vector3.MoveTowards(stick.transform.localPosition, 
            currentPoint, Time.deltaTime * (moveSpeed / 5));
    }
}
