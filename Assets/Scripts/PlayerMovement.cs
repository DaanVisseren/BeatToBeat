using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public Transform leftPos;
    public Transform midPos;
    public Transform rightPos;

    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode slash;
    public int currentLaneNr = 0;
    private int lastLaneNr = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastLaneNr = currentLaneNr;
        if (Input.GetKeyDown(moveRight))
        {
            currentLaneNr++;
            if (currentLaneNr >= 1)
            {
                currentLaneNr = 1;
            }
        }

        if (Input.GetKeyDown(moveLeft))
        {
            currentLaneNr--;
            if (currentLaneNr <= -1)
            {
                currentLaneNr = -1;
            }
        }

        if(currentLaneNr != lastLaneNr)
        {
            MovePlayer(currentLaneNr);
        }
    }

    public void MovePlayer(int laneNr)
    {
        if (laneNr == -1) { transform.position = leftPos.position; }
        if (laneNr == 0) { transform.position = midPos.position; }
        if (laneNr == 1) { transform.position = rightPos.position; }
    }
}
