using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathInstantiater : MonoBehaviour
{

    private LineRenderer lineRend;
    public Vector2 Beginning;
    public Vector2 End;

    void Start()
    {
        Debug.Log($"beginning: {Beginning}, end: {End}");
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.SetPosition(0, Beginning);
        lineRend.SetPosition(1, End); 
    }
}
