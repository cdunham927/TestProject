using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPaddleController : MonoBehaviour
{
    public bool p1;
    public float spd;
    Rigidbody2D bod;


    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (p1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                bod.AddForce(Vector2.up * spd * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                bod.AddForce(-Vector2.up * spd * Time.deltaTime);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                bod.AddForce(Vector2.up * spd * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                bod.AddForce(-Vector2.up * spd * Time.deltaTime);
            }
        }
    }
}
