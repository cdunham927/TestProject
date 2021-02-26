using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupEnemyController : MonoBehaviour
{
    public float maxHp;
    float hp;
    public float ySpd;
    public float xSpd;
    ShmupPlayerController player;
    Rigidbody2D bod;
    ShmupGameController cont;

    public float timeBetweenAttacksLow = 0.5f;
    public float timeBetweenAttacksHigh = 1f;
    public GameObject bullet;
    float attackCools;

    public bool attacks = true;

    public int scoreValue;
    public GameObject explosion;

    Vector2 bounds;

    private void OnEnable()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        cont = FindObjectOfType<ShmupGameController>();
        player = FindObjectOfType<ShmupPlayerController>();
        bod = GetComponent<Rigidbody2D>();
        hp = maxHp;
    }

    private void Update()
    {
        bod.AddForce(-Vector2.up * ySpd * Time.deltaTime);

        if (player != null && player.gameObject.activeInHierarchy)
        {
            if (player.transform.position.x > transform.position.x)
            {
                bod.AddForce(Vector2.right * xSpd * Time.deltaTime);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                bod.AddForce(Vector2.right * -xSpd * Time.deltaTime);
            }
        }

        if (attacks)
        {
            if (attackCools > 0) attackCools -= Time.deltaTime;

            if (attackCools <= 0) Attack();
        }

        if (transform.position.y < -bounds.y)
        {
            cont.AddScore(-scoreValue);
            gameObject.SetActive(false);
        }
    }

    void Attack()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        attackCools = Random.Range(timeBetweenAttacksLow, timeBetweenAttacksHigh);
    }

    public void TakeDamage(float amt)
    {
        hp -= amt;

        if (hp <= 0) Die();
    }

    void Die()
    {
        cont.AddScore(scoreValue);
        Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<ShmupPlayerController>().TakeDamage(2);
            gameObject.SetActive(false);
        }
    }
}
