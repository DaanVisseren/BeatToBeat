using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoints : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<Transform> deletePoints;

    public List<KeyCode> spawnKeycodes;
    public GameObject pointPrefab;
    public GameObject slashPointPrefab;
    public GameObject blankPointPrefab;
    public float timeToReachEnd;

    public int pointsSpawned = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnKeycodes[0])) {CreatePoint(0, pointPrefab); }
        if (Input.GetKeyDown(spawnKeycodes[1])) {CreatePoint(1, pointPrefab); }
        if (Input.GetKeyDown(spawnKeycodes[2])) {CreatePoint(2, pointPrefab); }

        if (Input.GetKeyDown(spawnKeycodes[3])) { CreatePoint(0, slashPointPrefab); }
        if (Input.GetKeyDown(spawnKeycodes[4])) { CreatePoint(1, slashPointPrefab); }
        if (Input.GetKeyDown(spawnKeycodes[5])) { CreatePoint(2, slashPointPrefab); }

        if (Input.GetKeyDown(spawnKeycodes[6])) { CreatePoint(0, blankPointPrefab); }
        if (Input.GetKeyDown(spawnKeycodes[7])) { CreatePoint(1, blankPointPrefab); }
        if (Input.GetKeyDown(spawnKeycodes[8])) { CreatePoint(2, blankPointPrefab); }
    }

    private void CreatePoint(int i, GameObject prefab)
    {
       GameObject obj = Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);
       HitPoint point = obj.GetComponent<HitPoint>();
       point.laneNr = i;
       point.pointNr = pointsSpawned;
       obj.GetComponent<MovePoint>().SetupPoint(timeToReachEnd);
       pointsSpawned++;
    }
}
