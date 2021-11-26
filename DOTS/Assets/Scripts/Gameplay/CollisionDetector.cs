using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private CircleCollider2D dotCircleCollider;
    public GameObject Grid;
    public Grid GridScript;

    public GameObject CanvasObject;

    public GameObject Ray;
    public Ray RayOrigin;
    private Transform thisTransform;
    private void Start()
    {
        dotCircleCollider = GetComponent<CircleCollider2D>();
        Grid = GameObject.Find("Grid");
        GridScript = Grid.GetComponent<Grid>();
        Ray = GameObject.Find("Ray");
        RayOrigin = Ray.GetComponent<Ray>();
        thisTransform = GetComponent<Transform>();
        CanvasObject = GameObject.Find("Canvas");
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"{this.name}");
        GridScript.clickedDots.Add(this.name);
        RayOrigin.PositionCollision = thisTransform;
        var CanvasObjectChild = CanvasObject.transform.GetChild(2).gameObject;
        CanvasObjectChild.SetActive(false);
    }
}
