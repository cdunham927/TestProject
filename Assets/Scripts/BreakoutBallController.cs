using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutBallController : MonoBehaviour
{
    Rigidbody2D bod;
    public float spd;
    public float randomUp;
    BreakoutGameController cont;
    Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
        cont = FindObjectOfType<BreakoutGameController>();
        bod = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("PushBall", 2f);
    }

    void PushBall()
    {
        int dir = Random.Range(0, 2);
        if (dir == 0) bod.AddForce(Vector2.right * spd);
        else bod.AddForce(Vector2.right * -spd);

        bod.AddForce(-Vector2.up * randomUp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 vel;
            vel.y = bod.velocity.y;
            vel.x = (bod.velocity.x / 2) + (collision.collider.attachedRigidbody.velocity.x / 3);
            bod.velocity = vel;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            cont.HitBrick();
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            cont.LoseHealth();

            bod.velocity = Vector2.zero;
            transform.position = startPos;
            Invoke("PushBall", 2f);
        }
    }
}
