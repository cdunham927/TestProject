using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEnemyController : MonoBehaviour, IDamageable<float>, IKillable
{
    public float maxHp;
    float hp;
    public float iframeTime = 0.2f;
    float iframes;
    GameObject[] spawnPoint;
    public enum enemystates { chase, attack }
    public enemystates curState;
    public float timeBetweenAttacks;
    float cools;
    public float spd;
    public float attackRange;
    float distance;
    int dir;
    public GameObject meleeObj;
    public float exp;
    public float money;

    SpriteRenderer rend;
    Animator anim;
    Rigidbody2D bod;
    ActionGameController cont;
    ActionPlayerController player;

    void Awake()
    {
        cont = FindObjectOfType<ActionGameController>();
        player = FindObjectOfType<ActionPlayerController>();
        anim = GetComponent<Animator>();
        bod = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        //spawnPoint = GameObject.FindGameObjectsWithTag("Checkpoint");
        //transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position;
        //transform.position = cont.spawnPoints[Random.Range(0, cont.spawnPoints.Length)].transform.position;

        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        switch(curState)
        {
            case enemystates.chase:
                Chase();
                break;
            case enemystates.attack:
                Attack();
                break;
        }

        anim.SetInteger("dir", dir);
        if (cools > 0) cools -= Time.deltaTime;
        if (iframes > 0) iframes -= Time.deltaTime;
    }

    void Chase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (player.transform.position.y < transform.position.y)
        {
            dir = 0;
            meleeObj.transform.localPosition = new Vector3(0, -1, 0);
            meleeObj.transform.localScale = new Vector3(1.35f, 1, 1);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            rend.flipX = false;
            dir = 1;
            meleeObj.transform.localPosition = new Vector3(1, 0, 0);
            meleeObj.transform.localScale = new Vector3(1, 1.35f, 1);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            rend.flipX = true;
            dir = 1;
            meleeObj.transform.localPosition = new Vector3(-1, 0, 0);
            meleeObj.transform.localScale = new Vector3(1, 1.35f, 1);
        }
        else if (player.transform.position.y > transform.position.y)
        {
            dir = 2;
            meleeObj.transform.localPosition = new Vector3(0, 1, 0);
            meleeObj.transform.localScale = new Vector3(1.35f, 1, 1);
        }

        if (distance > attackRange)
        {
            Vector3 direc = player.transform.position - transform.position;
            bod.AddForce(direc * Time.deltaTime * spd);
        }
        else
        {
            if (cools <= 0) curState = enemystates.attack;
        }
    }

    void Attack()
    {
        anim.SetBool("attacking", true);
        cools = timeBetweenAttacks;
        Invoke("ResetAttacking", 0.2f);
        curState = enemystates.chase;
    }

    void ResetAttacking()
    {
        anim.SetBool("attacking", false);
    }

    public void Damage(float amt)
    {
        if (iframes <= 0)
        {
            hp -= amt;

            //Debug.Log(hp + "/" + maxHp);
            if (hp <= 0) Die();
            iframes = iframeTime;
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        player.AddExp(exp);
        player.AddMoney(money);
    }
}
