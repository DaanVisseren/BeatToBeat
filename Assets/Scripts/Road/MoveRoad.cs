using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    public float speed;
    public GameObject roadPiece;

    public Transform roadEnd;
    private Vector3 roadEndPos;

    private bool roadSpawned = false;

    public Transform startpoint;
    public Transform endPoint;

    private void Start()
    {
        roadEndPos = roadEnd.position; 
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
        if(transform.position.x >= endPoint.position.x){
            Destroy(gameObject);
        }

        if(transform.position.x >= roadEndPos.x && !roadSpawned)
        {
            roadSpawned = true;
            SpawnRoadPiece();
        }
    }

    public void SpawnRoadPiece()
    {
        GameObject obj = Instantiate(roadPiece, startpoint.position, Quaternion.identity);
        obj.name = "RoadPiece";
        MoveRoad newRoadMover = obj.GetComponent<MoveRoad>();
        newRoadMover.startpoint = startpoint;
        newRoadMover.endPoint = endPoint;
    }
}
