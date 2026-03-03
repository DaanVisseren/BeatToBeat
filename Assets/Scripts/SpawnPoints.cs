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

    private int lastLanePlaced = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Check Lane 1
        if (RhythmAudioManager.Instance.ShouldSpawnLane1())
        {
            CreatePoint(0, slashPointPrefab);
            Debug.Log("Should spawn in lane 1");
        }

        // Check Lane 2
        if (RhythmAudioManager.Instance.ShouldSpawnLane2())
        { CreatePoint(1, slashPointPrefab); }

        // Check Lane 3
        if (RhythmAudioManager.Instance.ShouldSpawnLane3())
        { CreatePoint(2, slashPointPrefab); }


        //Temp
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
        if (point.pointType == 0) {RotateObject(obj, point); }//TEMP 

       point.pointNr = pointsSpawned;
       obj.GetComponent<MovePoint>().SetupPoint(timeToReachEnd);
       pointsSpawned++;
       lastLanePlaced = i;
    }

    //TEMP
    public void RotateObject(GameObject obj, HitPoint point)
    {
        if(pointsSpawned == 1 && point.laneNr == 2)
        {
            obj.transform.localRotation *= Quaternion.Euler(0, 180, -23);
            return;
        }

        if((point.laneNr == 1 && lastLanePlaced == 0) || point.laneNr == 2)
        {
            obj.transform.localRotation *= Quaternion.Euler(0, 180, -23);
        }
    }
}
