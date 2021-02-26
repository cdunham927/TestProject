using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDBulletController : MonoBehaviour
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

        Invoke("Disable", 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !hasHurt)
        {
            collision.GetComponent<TDEnemyController>().TakeDamage(damage);
            hasHurt = true;
            Invoke("Disable", 0.001f);
        }
        if (collision.CompareTag("Wall"))
        {
            Invoke("Disable", 0.001f);
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
