using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupPlayerController : MonoBehaviour
{
    [Header("Starting stats and shooting stuff")]
    public float spd;
    Vector2 input;
    Rigidbody2D bod;
    public GameObject bullet;
    public GameObject[] bulletSpawn;
    float cools;
    public float timeBetweenShots = 0.3f;

    [Header("Health things n shit")]
    public int maxHp;
    int hp;
    public GameObject hpImage;
    public GameObject healthParent;

    [Header("Iframes")]
    public float iframeTime = 0.3f;
    float iframes;

    public GameObject flash;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();

        hp = maxHp;
        for (int i = 0; i < hp; i++)
        {
            AddHeart();
        }
    }

    void AddHeart()
    {
        GameObject h = Instantiate(hpImage);
        h.transform.SetParent(healthParent.transform);
    }

    void RemoveHeart(int i)
    {
        hp--;
        if (healthParent.transform.childCount > i) Destroy(healthParent.transform.GetChild(i).gameObject);
        else Destroy(healthParent.transform.GetChild(0).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.x != 0)
        {
            bod.AddForce(Vector2.right * spd * input.x * Time.deltaTime);
        }
        if (input.y != 0)
        {
            bod.AddForce(Vector2.up * spd * input.y * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space) && cools <= 0)
        {
            Shoot();
        }

        if (cools > 0) cools -= Time.deltaTime;
        if (iframes > 0) iframes -= Time.deltaTime;
    }

    public void TakeDamage(int dmg)
    {
        if (iframes <= 0)
        {
            for (int i = dmg; i > 0; i--)
            {
                RemoveHeart(i - 1);

                if (hp <= 0) Die();
            }

            //bloodParticles.Emit(burstAmt);
            iframes = iframeTime;
        }
    }

    void Die()
    {
        FindObjectOfType<ShmupGameController>().ActivateDeathUI();
        gameObject.SetActive(false);
    }

    void Shoot()
    {
        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            Instantiate(bullet, bulletSpawn[i].transform.position, Quaternion.identity);
            Instantiate(flash, bulletSpawn[i].transform.position, Quaternion.identity);
        }

        cools = timeBetweenShots;
    }
}
