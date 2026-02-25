using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public Transform endPoint;
    public float smooth = 1f;
    Vector3 currentVelocity;

    void Start()
    {
        
    }


    void Update()
    {
        Vector3 dist = endPoint.position - transform.position;

        if (dist.x > 5f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, endPoint.position, ref currentVelocity, smooth);
        }

        float distance = Vector3.Distance(endPoint.position, transform.position);
        if (distance < 0.5f){
            Destroy(gameObject);
        }
    }
}
