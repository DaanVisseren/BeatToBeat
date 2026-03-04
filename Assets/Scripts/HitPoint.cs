using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public float hitWindow;
    public float negativeHitWindow;
    public float laneNr;
    public MovePoint pointMover;
    public int pointNr;
    private PlayerMovement player;
    public int pointType;
    public float blankHitWindow;
    public float negativeBlankHitWindow;
    public PointSystem pointSystem;
    private float maxDistanceFromPlayerToScore = 12f;
    private bool missed = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        pointSystem = GameObject.FindWithTag("PointSystem").GetComponent<PointSystem>();
        hitWindow = pointSystem.maxTimeOff;
        negativeHitWindow = pointSystem.negativeMaxTimeOff;
    }

    void Update()
    {
        MissPoint();

        if(pointType == 2)
        {
            if (pointMover.timeToPlayer <= blankHitWindow && pointMover.timeToPlayer >= negativeBlankHitWindow)
            {
                TryToHitPoint(player.currentLaneNr, pointType);
            }
        }
    }

    public void TryToHitPoint(int lane, int typeTryingToHit)
    {
        if (player.notDisable())
        {
            if ((pointMover.timeToPlayer >= negativeHitWindow && pointMover.timeToPlayer <= hitWindow) || pointType == 2)
            {
                if (lane == laneNr)
                {
                    if (pointType == typeTryingToHit)
                    {
                        Debug.Log(pointMover.timeToPlayer);
                        player.currentPointNr++;
                        if (pointType == 2) { pointSystem.GainPoints(); }
                        else { pointSystem.GainPoints(pointMover.timeToPlayer); }
                        Destroy(gameObject);
                    }
                }
            }
        }
    }


    public void MissPoint()
    {
        if (pointMover.timeToPlayer < (negativeHitWindow -0.002f) && !missed)
        {
            player.currentPointNr++;
            pointSystem.BreakStreak();
            missed = true;
        }

        if (transform.position.x > maxDistanceFromPlayerToScore)
        {
            Destroy(gameObject);
        }
    }

    public float CalculateTimeOff(float timeToPlayer)
    {
        return Mathf.Abs(timeToPlayer);
    }
}
