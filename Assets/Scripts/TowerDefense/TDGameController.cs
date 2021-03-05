using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TDGameController : MonoBehaviour
{
    public BaseTower tower;
    [HideInInspector]
    public float currentTowerCost;
    public Transform[] waypoints;
    public float maxHp;
    float hp;
    public Image healthImage;
    public float lerpSpd;
    public float money;
    public Text moneyText;
    public BaseTower[] towers;
    [Space]
    public float timeBetweenSpawnsLow;
    public float timeBetweenSpawnsHigh;
    public float timeBetweenWavesLow;
    public float timeBetweenWavesHigh;
    public int enemiesPerWaveLow;
    public int enemiesPerWaveHigh;
    float cools;
    float waveCools;
    int enemiesToSpawn;
    int enemiesThisWave = 0;
    public GameObject spawn;
    public GameObject[] enemies;
    bool isAlive;
    float resetCools = 0.3f;
    public GameObject deadUI;

    private void Awake()
    {
        hp = maxHp;
        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, hp / maxHp, lerpSpd * Time.deltaTime);
        isAlive = true;
        UpdateTower(0);
        InvokeRepeating("UpEnemyRate", 5f, 10f);
        resetCools = 0.3f;
    }

    void UpEnemyRate()
    {
        enemiesPerWaveLow++;
        enemiesPerWaveHigh++;

        if (timeBetweenWavesLow > 2f)
        {
            timeBetweenWavesLow -= 0.3f;
            timeBetweenWavesHigh -= 0.3f;

        }
    }

    public void SpawnEnemy()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)], spawn.transform.position, Quaternion.Euler(0, 0, -90));
        enemiesThisWave++;
        cools = Random.Range(timeBetweenSpawnsLow, timeBetweenSpawnsHigh);
    }

    public void UpdateTower(int num)
    {
        tower = towers[num];
        currentTowerCost = towers[num].cost;
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Time.timeScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Time.timeScale *= 2f;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                TakeDamage(10);
            }
        }
        if (isAlive)
        {
            if (cools > 0) cools -= Time.deltaTime;
            if (waveCools > 0) waveCools -= Time.deltaTime;

            if (cools <= 0 && enemiesThisWave < enemiesToSpawn)
            {
                SpawnEnemy();
            }

            if (waveCools <= 0)
            {
                enemiesThisWave = 0;

                enemiesToSpawn = Random.Range(enemiesPerWaveLow, enemiesPerWaveHigh);
                waveCools = Random.Range(timeBetweenWavesLow, timeBetweenWavesHigh);
            }
        }

        if (!isAlive)
        {
            if (resetCools > 0) resetCools -= Time.deltaTime;

            if (resetCools <= 0 && Input.anyKey)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        moneyText.text = "Money: " + money.ToString();
        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, hp / maxHp, lerpSpd * Time.deltaTime);
    }

    public void GiveMoney(float amt)
    {
        money += amt;
    }

    public void TakeDamage(float amt)
    {
        hp -= amt;
        if (hp <= 0)
        {
            isAlive = false;
            deadUI.SetActive(true);
        }
    }
}
