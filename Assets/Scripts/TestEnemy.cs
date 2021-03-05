using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    Animator anim;
    int hp;
    public int maxHp;
    public float spd;
    Rigidbody2D bod;
    PlayerController player;
    public float attackRange;
    float distance;
    float attackCools;
    public float timeBetweenAttacks = 0.3f;
    public GameObject enemyProjectile;
    float iframes;
    public float chaseRange;
    Vector3 startPos;
    Vector3 wanderPos;
    bool chasing;
    public float wanderRadius;
    float wanderDistance;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bod = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        chasing = false;
        startPos = transform.position;
        wanderPos = transform.position * Random.insideUnitCircle * Random.Range(0, wanderRadius);

        hp = maxHp;
    }

    private void Update()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            /*if (distance >= attackRange && distance <= chaseRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, spd * Time.deltaTime);
            }*/
            if (distance >= attackRange && chasing)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, spd * Time.deltaTime);
            }
            if (distance < attackRange & attackCools <= 0)
            {
                Attack();
            }

            //if (distance > chaseRange) transform.position = Vector3.MoveTowards(transform.position, startPos, spd * Time.deltaTime);
            if (!chasing) transform.position = Vector3.MoveTowards(transform.position, wanderPos, spd * Time.deltaTime);
            wanderDistance = Vector2.Distance(transform.position, wanderPos);
            if (!chasing && wanderDistance <= 0.1f) wanderPos = transform.position * Random.insideUnitCircle * Random.Range(0, wanderRadius);
        }

        if (attackCools > 0) attackCools -= Time.deltaTime;
        if (iframes > 0) iframes -= Time.deltaTime;
    }

    void Attack()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Instantiate(enemyProjectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        attackCools = timeBetweenAttacks;
    }

    public void TakeDamage(int dmg)
    {
        if (iframes <= 0)
        {
            anim.Play("SquidHurt");
            hp -= dmg;

            if (hp <= 0) Disable();
            iframes = 0.3f;
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        anim.Play("SquidHurt");

        TakeDamage(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chasing = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chasing = false;
            wanderPos = transform.position * Random.insideUnitCircle * Random.Range(0, wanderRadius);
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, wanderPos);
        }
    }
}
