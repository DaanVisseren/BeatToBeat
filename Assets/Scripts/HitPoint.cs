using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public float hitWindow;
    public float laneNr;
    public MovePoint pointMover;
    public int pointNr;
    private PlayerMovement player;
    public int pointType;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        MissPoint();

        if(pointType == 2)
        {
            TryToHitPoint(player.currentLaneNr, pointType);
        }
    }

    public void TryToHitPoint(int lane, int typeTryingToHit)
    {
        if ((pointMover.timeToPlayer >= hitWindow * -1 && pointMover.timeToPlayer <= hitWindow))
        {
            if (lane == laneNr)
            {
                if (pointType == typeTryingToHit)
                {
                    Debug.Log(pointMover.timeToPlayer);
                    player.currentPointNr++;
                    Destroy(gameObject);
                }
            }
        }
    }


    public void MissPoint()
    {
        if (pointMover.timeToPlayer < hitWindow - ((hitWindow*2)-0.002f))
        {
            player.currentPointNr++;
            Debug.Log("Missed!");
            Destroy(gameObject);
        }
    }
}
