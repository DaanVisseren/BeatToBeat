using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public float speed;
    public Transform axis;

    void Update()
    {
        transform.RotateAround(axis.position, Vector3.forward, Time.deltaTime * speed);
    }
}
