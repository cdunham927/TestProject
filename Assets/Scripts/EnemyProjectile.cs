using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float spd;
    Rigidbody2D bod;
    public int damage = 1;
    bool hasHurt;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        hasHurt = false;
        bod.AddForce(transform.up * spd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasHurt)
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            hasHurt = true;
            gameObject.SetActive(false);
        }
    }
}
