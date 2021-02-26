using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollABallPlayer : MonoBehaviour
{
    public float spd;
    Vector2 input;
    Rigidbody bod;

    private void Awake()
    {
        bod = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.x != 0)
        {
            bod.AddForce(Vector3.right * spd * input.x * Time.deltaTime);
        }
        if (input.y != 0)
        {
            bod.AddForce(Vector3.forward * spd * input.y * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}
