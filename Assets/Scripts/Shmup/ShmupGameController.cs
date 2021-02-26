using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShmupGameController : MonoBehaviour
{
    ShmupPlayerController player;
    public GameObject[] enemies;
    public float timeBetweenSpawnsLow;
    public float timeBetweenSpawnsHigh;
    float spawnCools;
    Vector2 bounds;
    Vector3 spawnPoint;
    public float timeDecAmt;
    public int score = 0;
    public Text scoreText;
    public GameObject deathUI;

    private void Awake()
    {
        player = FindObjectOfType<ShmupPlayerController>();
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        score = 0;
    }

    public void ActivateDeathUI()
    {
        deathUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                player.TakeDamage(1);
            }
        }

        if (spawnCools > 0) spawnCools -= Time.deltaTime;

        if (spawnCools <= 0)
        {
            SpawnEnemy();
        }

        timeBetweenSpawnsLow -= Time.deltaTime * timeDecAmt;
        timeBetweenSpawnsHigh -= Time.deltaTime * timeDecAmt;

        timeBetweenSpawnsLow = Mathf.Clamp(timeBetweenSpawnsLow, 0.1f, 5f);
        timeBetweenSpawnsHigh = Mathf.Clamp(timeBetweenSpawnsHigh, 0.1f, 5f);

        scoreText.text = "Score: " + score.ToString();
    }

    public void AddScore(int amt)
    {
        score += amt;
    }

    void SpawnEnemy()
    {
        //Spawns above player
        spawnPoint = new Vector3(Random.Range(-bounds.x + 1f, bounds.x - 1f), bounds.y + Random.Range(0.25f, 3f), 0f);
        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint, Quaternion.Euler(0, 0, 180));
        spawnCools = Random.Range(timeBetweenSpawnsLow, timeBetweenSpawnsHigh);
    }
}
