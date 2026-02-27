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
    public float blankHitWindow;
    public PointSystem pointSystem;
    private float maxDistanceFromPlayerToScore = 6.14f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        pointSystem = GameObject.FindWithTag("PointSystem").GetComponent<PointSystem>();
        hitWindow = pointSystem.maxTimeOff;
    }

    void Update()
    {
        MissPoint();

        if(pointType == 2)
        {
            if (pointMover.timeToPlayer <= blankHitWindow)
            {
                TryToHitPoint(player.currentLaneNr, pointType);
            }
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
                    pointSystem.GainPoints(CalculateTimeOff(pointMover.timeToPlayer));
                    Destroy(gameObject);
                }
            }
        }
    }

    public void MissPoint()
    {
        if (pointMover.timeToPlayer < hitWindow - ((hitWindow*2)-0.002f) || transform.position.x > maxDistanceFromPlayerToScore)
        {
            player.currentPointNr++;
            Debug.Log("Missed!");
            pointSystem.BreakStreak();
            Destroy(gameObject);
        }
    }

    public float CalculateTimeOff(float timeToPlayer)
    {
        return Mathf.Abs(timeToPlayer);
    }
}
