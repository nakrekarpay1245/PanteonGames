using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawableCube : MonoBehaviour
{
    private bool isDrawed;

    public bool GetIsDrawed()
    {
        return isDrawed;
    }
    public void SetIsDrawed(bool value)
    {
        isDrawed = value;
    }
}
