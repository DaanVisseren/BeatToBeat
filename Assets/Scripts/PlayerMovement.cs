using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public Transform leftPos;
    public Transform midPos;
    public Transform rightPos;

    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode slash;
    public int currentLaneNr = 1;
    private int lastLaneNr = 1;

    public int currentPointNr = 0;

    public bool snapBack = false;

    void Start()
    {
        
    }

    void Update()
    {
        lastLaneNr = currentLaneNr;


        if (!snapBack)
        {
            if (Input.GetKeyDown(moveRight))
            {
                changeLaneNr(true);
            }

            if (Input.GetKeyDown(moveLeft))
            {
                changeLaneNr(false);
            }
        }
        else
        {
            if (Input.GetKeyDown(moveRight))
            {
                changeLaneNr(true);
            }
            else if(Input.GetKeyUp(moveRight))
            {
                changeLaneNr(false);
            }

            if (Input.GetKeyDown(moveLeft))
            {
                changeLaneNr(false);
            }
            else if (Input.GetKeyUp(moveLeft))
            {
                changeLaneNr(true);
            }
        }

        if (Input.GetKeyDown(slash))
        {
            if (FindPoint() != null)
            {
                FindPoint().TryToHitPoint(currentLaneNr, 1);
            }
        }

        if(currentLaneNr != lastLaneNr)
        {
            MovePlayer(currentLaneNr);
        }
    }

    public void changeLaneNr(bool right)
    {
        if(right)
        {
            currentLaneNr++;
            if (currentLaneNr > 2)
            {
                currentLaneNr = 2;
            }
            else
            {
                if(FindPoint() != null)
                {
                    FindPoint().TryToHitPoint(currentLaneNr, 0);
                }
            }
        }
        else
        {
            currentLaneNr--;
            if (currentLaneNr < 0)
            {
                currentLaneNr = 0;
            }
            else
            {
                if (FindPoint() != null)
                {
                    Debug.Log("Found one");
                    FindPoint().TryToHitPoint(currentLaneNr, 0);
                }
            }
        }
    }

    public HitPoint FindPoint()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Point");
        foreach (GameObject obj in objects)
        {
            HitPoint point = obj.GetComponent<HitPoint>();
            if (point.pointNr == currentPointNr)
            {
                return point;
            }
        }
        return null;
    }
    public void MovePlayer(int laneNr)
    {
        if (laneNr == 0) { transform.position = leftPos.position; }
        if (laneNr == 1) { transform.position = midPos.position; }
        if (laneNr == 2) { transform.position = rightPos.position; }
    }
}
