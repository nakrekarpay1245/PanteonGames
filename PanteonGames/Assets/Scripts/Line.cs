using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private List<Vector3> points;

    private float nextTimeToMousePosition = 0;
    private float mousePositionRate = 0.025f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void UpdateLine(Vector3 mousePosition)
    {
        if (points == null)
        {
            points = new List<Vector3>();
            SetPoint(mousePosition);
            return;
        }

        if (Time.time > nextTimeToMousePosition)
        {
            nextTimeToMousePosition = Time.time + mousePositionRate;

            SetPoint(mousePosition);
        }
    }

    private void SetPoint(Vector2 mousePosition)
    {
        points.Add(mousePosition);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, mousePosition);
    }
}