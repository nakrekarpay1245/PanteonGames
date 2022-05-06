using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float moveSpeed;

    [SerializeField]
    private Vector3[] points;

    private Vector3 currentPoint;
    private int pointIndex;

    private void Start()
    {
        currentPoint = points[0];
        moveSpeed = Random.Range(speed/3, speed * 2);
        for(int i =0; i<points.Length; i++)
        {
            points[i] = new Vector3(points[i].x, transform.position.y, transform.position.z);
        }
    }

    void Update()
    {
        currentPoint = points[pointIndex % points.Length];

        if (Vector3.Distance(transform.position, currentPoint) < 0.5f)
        {
            pointIndex++;
        }

        transform.position = Vector3.MoveTowards(transform.position,
            currentPoint, Time.deltaTime * moveSpeed);
    }
}
