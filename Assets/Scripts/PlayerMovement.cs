using Assets.Scripts.Objects;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform leftPos;
    public Transform midPos;
    public Transform rightPos;

    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode slash;
    public KeyCode crash;
    public int currentLaneNr = 1;
    private int lastLaneNr = 1;

    public int currentPointNr = 0;

    public float moveSpeed;
    public bool snapBack = false;

    private float t = 0;
    public bool isMoving = false;
    private Vector3 endPoint;
    private Vector3 startPoint;

    public bool disabled => Time.unscaledTime < disabledUntilTime;
    private float disabledUntilTime;
    [SerializeField] private float spinDuration = 0.5f;
    private bool isSpinning = false;

    private void Start()
    {
    }
    void Update()
    {
        if(notDisable())
        {
            if (Input.GetKeyDown(crash))
            {
                HitObstical();
            }
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
                else if (Input.GetKeyUp(moveRight))
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
                    FindPoint().TryToHitPoint(currentLaneNr, BeatTypes.Standard);
                }
            }

            if (currentLaneNr != lastLaneNr)
            {
                SetMovement(currentLaneNr);
            }

            if (isMoving)
            {
                MovePlayer();
            }
        }
        lastLaneNr = currentLaneNr;

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
                    FindPoint().TryToHitPoint(currentLaneNr, BeatTypes.Side);
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
                    FindPoint().TryToHitPoint(currentLaneNr, BeatTypes.Side);
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
    public void MovePlayer()
    {
        if(transform.position == endPoint)
        {
            isMoving = false;
            return;
        }
        t += Time.deltaTime / moveSpeed;
        transform.position = Vector3.Lerp(startPoint, endPoint, t);
    }

    public void SetMovement(int laneNr)
    {
        if (!isMoving)
        {
            t = 0;
        }

        startPoint = transform.position;

        if (laneNr == 0) { /*transform.position = leftPos.position; */ endPoint = leftPos.position; }
        if (laneNr == 1) { /*transform.position = midPos.position; */ endPoint = midPos.position;  }
        if (laneNr == 2) { /*transform.position = rightPos.position; */ endPoint = rightPos.position;  }

        isMoving = true;
    }
    public bool notDisable()
    {
        return !disabled;
    }
    public void HitObstical()
    {
        DisableForMilliseconds(500);
        Spin360();
    }
    public void DisableForMilliseconds(int milliseconds)
    {
        disabledUntilTime = Time.unscaledTime + (milliseconds / 1000f);
    }

    private IEnumerator Spin360()
    {
        isSpinning = true;

        float elapsed = 0f;
        float startRotation = transform.eulerAngles.z;
        float targetRotation = startRotation + 360f;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / spinDuration;

            float currentRotation = Mathf.Lerp(startRotation, targetRotation, progress);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            yield return null;
        }

        // Snap exactly to final rotation
        transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);

        isSpinning = false;
    }

}
