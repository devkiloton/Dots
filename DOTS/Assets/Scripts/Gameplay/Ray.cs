using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    public Transform PositionCollision;
    public Transform CursorTransform;

    void Start()
    {
        
    }

    void Update()
    {
        if(PositionCollision!=null)
        {
            Ray2D(PositionCollision, CursorTransform);
        }
    }

    public void Ray2D(Transform positionCollision,Transform cursorTransform)
    {
        //Ray2D ray = new Ray2D( positionCollision.position, cursorTransform.position);
        Debug.DrawLine(positionCollision.position, cursorTransform.position, Color.red);
        //Debug.Log($"posiitioncollision{PositionCollision.position},cursorTransform{CursorTransform.position}");
    }
}