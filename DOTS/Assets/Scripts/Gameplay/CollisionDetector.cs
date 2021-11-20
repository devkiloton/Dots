using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private CircleCollider2D dotCircleCollider;

    private void Start()
    {
        dotCircleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{this.name}");
    }
}
