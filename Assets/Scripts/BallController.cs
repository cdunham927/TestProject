using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D bod;
    public float spd;
    public float randomUp;
    PongGameController cont;

    private void Awake()
    {
        cont = FindObjectOfType<PongGameController>();
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

        bod.AddForce(Vector2.up * Random.Range(-randomUp, randomUp));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 vel;
            vel.x = bod.velocity.x;
            vel.y = (bod.velocity.y / 2) + (collision.collider.attachedRigidbody.velocity.y / 3);
            bod.velocity = vel;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            if (bod.velocity.x > 0)
            {
                //Player 1 scored
                cont.Score(true);
            }
            else
            {
                //Player 2 scored
                cont.Score(false);
            }

            bod.velocity = Vector2.zero;
            transform.position = new Vector3(0, 0, 0);
            Invoke("PushBall", 2f);
        }
    }
}
