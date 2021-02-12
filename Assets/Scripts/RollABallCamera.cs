using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollABallCamera : MonoBehaviour
{
    public Transform target;
    public float lerpSpd;
    Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x + target.position.x, startPos.y + target.position.y, startPos.z + target.position.z), lerpSpd * Time.fixedDeltaTime);
    }
}
