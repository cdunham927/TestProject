using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutPaddleController : MonoBehaviour
{
    Rigidbody2D bod;
    public float spd;
    float input;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input = Input.GetAxis("Horizontal");

        if (input != 0)
        {
            bod.AddForce(Vector2.right * input * spd * Time.deltaTime);
        }
    }
}
