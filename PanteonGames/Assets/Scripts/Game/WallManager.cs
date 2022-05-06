using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallManager : MonoBehaviour
{
    [Header("Wall Child's")]
    [SerializeField]
    private List<GameObject> cubes;

    public List<GameObject> drawedCubes;

    [Header("Variables")]
    [SerializeField]
    private float drawTime = 25;

    private int drawedCubesCount;

    public static WallManager wallManager;
    private void Awake()
    {
        if (wallManager == null)
        {
            wallManager = this;
        }
        drawTime = 0;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Mouse 0");
            if (Physics.Raycast(ray, out hit, 100))
            {
                //Debug.Log("Raycast");

                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    //Debug.Log("Ray Touched The Wall");

                    if (CanIDraw())
                    {
                        //Debug.Log("I Can Draw");
                        drawTime -= Time.deltaTime;
                        UserInterfaceManager.userInterfaceManager.ShowDrawTime(drawTime);
                        //Debug.Log("Name : " + hit.collider.gameObject.name);
                        IncreaseDrawedCubes(hit.collider.gameObject);
                        hit.collider.gameObject.GetComponent<DrawableCube>().SetIsDrawed(true);
                    }
                    else
                    {
                        //Debug.Log("Get Well You Soon!");
                        UserInterfaceManager.userInterfaceManager.ShowResults();
                    }
                }
                else
                {
                    //Debug.Log("Ray Can Touch The: " + hit.collider.gameObject.name);
                }
            }
        }
    }

    public bool CanIDraw()
    {
        return drawTime > 0;
    }

    public void IncreaseDrawedCubes(GameObject cube)
    {
        if (!cube.GetComponent<DrawableCube>().GetIsDrawed())
        {
            drawedCubes.Add(cube);
            drawedCubesCount++;
            //Debug.Log("Drawed Cubes Count : " + drawedCubesCount);
            //Debug.Log("Drawed Cubes Static : " +
            //100 * ((float)drawedCubesCount / (float)cubes.Count) + " % ");
            float drawPercentage = 0;
            drawPercentage = 100 * ((float)drawedCubesCount / (float)cubes.Count);
            //Debug.Log("DP : " + drawPercentage);
            UserInterfaceManager.userInterfaceManager.ShowDrawPercentage(drawPercentage);
            if (drawPercentage >= 99)
            {
                UserInterfaceManager.userInterfaceManager.ShowResults();
            }
        }
    }
    public void PaintState(float value)
    {
        drawTime = value;
    }
}
