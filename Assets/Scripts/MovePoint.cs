using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPoint;
    public float distanceToEnd = 30f;
    public float timeToReachTarget = 7f;
    public float hitWindow = 0.150f;
    public float t;
    public float timeToPlayer;

    public float timeAlive = 0;

    private bool ready = false;
    public void SetupPoint(float timeToTarget)
    {
        t = 0;
        startPosition = transform.position;
        startPosition.x = -20;

        endPoint = transform.position;
        endPoint.x = distanceToEnd;

        timeToReachTarget = timeToTarget;
        timeToPlayer = timeToReachTarget / 2;

        ready = true;
    }

    void Update()
    {
        if (ready)
        {
            timeAlive += Time.deltaTime;
            timeToPlayer -= Time.deltaTime;
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startPosition, endPoint, t);
        }  
    }

    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        endPoint = destination;
    }



}
