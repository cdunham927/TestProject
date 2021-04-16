using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float lowSpd;
    public float highSpd;
    float spd;
    Rigidbody2D bod;
    public int damage = 1;
    bool hasHurt;
    public ParticleSystem particlePrefab;
    public ParticleSystem particleInstance;
    public int partAmt;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
        particleInstance = Instantiate(particlePrefab);
        particleInstance.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        hasHurt = false;
        spd = Random.Range(lowSpd, highSpd);
        bod.AddForce(transform.up * spd);

        Invoke("Disable", 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !hasHurt)
        {
            collision.GetComponent<TestEnemy>().TakeDamage(damage);
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
        particleInstance.transform.position = transform.position;
        particleInstance.transform.rotation = transform.rotation;
        particleInstance.gameObject.SetActive(true);
        particleInstance.Emit(partAmt);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
