using Assets.Scripts.Objects;
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
    public GameObject breakPointPrefab;
    public GameObject slidePointPrefab;
    public float timeToReachEnd;

    public int pointsSpawned = 0;

    private int lastLanePlaced = 10;


    public bool autoSpawn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (autoSpawn)
        {
            var beats1 = RhythmAudioManager.Instance.ShouldSpawnLane1();
            foreach (var beat in beats1)
                spawn(0, beat.BeatTypes);

            var beats2 = RhythmAudioManager.Instance.ShouldSpawnLane2();
            foreach (var beat in beats2)
                spawn(1, beat.BeatTypes);

            var beats3 = RhythmAudioManager.Instance.ShouldSpawnLane3();
            foreach (var beat in beats3)
                spawn(2, beat.BeatTypes);
        }



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

        if (Input.GetKeyDown(spawnKeycodes[9])) { CreatePoint(0, breakPointPrefab); }
        if (Input.GetKeyDown(spawnKeycodes[10])) { CreatePoint(1, breakPointPrefab); }
        if (Input.GetKeyDown(spawnKeycodes[11])) { CreatePoint(2, breakPointPrefab); }
    }

    public void CreatePoint(int i, GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);
        HitPoint point = obj.GetComponent<HitPoint>();

        point.laneNr = i;
        if (point.beatType == BeatTypes.Side) {RotateObject(obj, point); }//TEMP 

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

    public void spawn(int lane, BeatTypes type)
    {
        if (type == (BeatTypes)0)
        {
            Debug.Log("Create point");
            CreatePoint(lane, slashPointPrefab);
        }
        else
            Debug.LogWarning($"Attempted to spawn on invalid lane {lane}");
        if (type == (BeatTypes)1)
        {
            Debug.Log("Create point");
            CreatePoint(lane, slidePointPrefab);
        }
        else
            Debug.LogWarning($"Attempted to spawn on invalid lane {lane}");
        if (type == (BeatTypes)2)
        {
            Debug.Log("Create point");
            CreatePoint(lane, blankPointPrefab);
        }
        else
            Debug.LogWarning($"Attempted to spawn on invalid lane {lane}");
        if (type == (BeatTypes)3)
        {
            Debug.Log("Create point");
            CreatePoint(lane, breakPointPrefab);
        }
        else
            Debug.LogWarning($"Attempted to spawn on invalid lane {lane}");
    }
}
