using Assets.Scripts.Objects;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeRoadSpeed : MonoBehaviour
{
    public float speed;
    private float lastSpeed;

    private void Start()
    {
        lastSpeed = 0;
        ChangeAllRoadPiecesSpeed(speed);
    }
    private void Update()
    {    
        if(lastSpeed != speed)
        {
            ChangeAllRoadPiecesSpeed(speed);
        }
        lastSpeed = speed;
    }
    public void ChangeAllRoadPiecesSpeed(float newSpeed)
    {
        foreach(GameObject road in FindAllRoadPieces())
        {
            road.GetComponent<MoveRoad>().speed = newSpeed;
        }
    }

    public GameObject[] FindAllRoadPieces()
    {
        return GameObject.FindGameObjectsWithTag("Road");
    }
}
