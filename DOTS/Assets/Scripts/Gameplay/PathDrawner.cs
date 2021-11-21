using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawner : MonoBehaviour
{
    private LineRenderer lineRend;

    //public GameObject PathInstantiaterObject;
    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PathMaker(Vector2 beginning, Vector2 end)
    {
        lineRend.SetPosition(0, beginning);
        lineRend.SetPosition(1, end); 
    }
}
