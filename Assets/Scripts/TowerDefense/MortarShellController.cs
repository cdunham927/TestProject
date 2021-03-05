using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShellController : MonoBehaviour
{
    public float spd;
    Rigidbody2D bod;
    public int damage = 1;
    bool hasHurt;
    public float radius;
    public LayerMask enemyMask;

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
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius, enemyMask);
            foreach (Collider2D col in hit)
            {
                col.GetComponent<TDEnemyController>().TakeDamage(damage);
            }
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
