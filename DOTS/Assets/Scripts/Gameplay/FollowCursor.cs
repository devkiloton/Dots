using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public Ray RayDirection;
    private Transform thisTransform;
    void Start()
    {

    }
    void Update()
    {
        Vector2 position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = position;
        thisTransform = GetComponent<Transform>();
        RayDirection.CursorTransform = thisTransform;
    }
}
