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

    ActionGameController cont;

    void OnEnable()
    {
        //spawnPoint = GameObject.FindGameObjectsWithTag("Checkpoint");
        //transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position;
        //cont = FindObjectOfType<ActionGameController>();
        //transform.position = cont.spawnPoints[Random.Range(0, cont.spawnPoints.Length)].transform.position;

        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (iframes > 0) iframes -= Time.deltaTime;
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
    }
}
