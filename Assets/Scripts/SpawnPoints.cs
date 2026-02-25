using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoints : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<Transform> deletePoints;

    public List<KeyCode> spawnKeycodes;

    public GameObject pointPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnKeycodes[0])) {CreatePoint(0); }
        if (Input.GetKeyDown(spawnKeycodes[1])) {CreatePoint(1); }
        if (Input.GetKeyDown(spawnKeycodes[2])) {CreatePoint(2); }
    }

    private void CreatePoint(int i)
    {
       GameObject obj = Instantiate(pointPrefab, spawnPoints[i].position, Quaternion.identity);
        obj.GetComponent<MovePoint>().endPoint = deletePoints[i];
    }
}
