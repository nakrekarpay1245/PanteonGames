using UnityEngine;
using System.Collections.Generic;

public class LineCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject linePrefab;

    private GameObject currentLine;

    private Line activeLine;

    public static LineCreator instance;
    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    if (WallManager.wallManager.CanIDraw())
                    {
                        currentLine = Instantiate(linePrefab);
                        activeLine = currentLine.GetComponent<Line>();
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Destroy(currentLine);
            activeLine = null;
        }

        if (activeLine != null)
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    if (WallManager.wallManager.CanIDraw())
                    {
                        //Debug.Log("Hit Point : " + hit.point);
                        Vector3 mousePosition = hit.point;
                        activeLine.UpdateLine(mousePosition);
                    }
                    else
                    {
                        //Debug.Log("Get Well You Soon!");
                    }
                }
            }
        }
    }
}
