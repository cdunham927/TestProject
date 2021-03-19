using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //public int money;
    //public List<GameObject> enemies = new List<GameObject>();
    public GameObject[] enemyArray;
    public float timeBetweenSpawns;
    float spawnCools;
    Vector2 bounds;
    Vector3 spawnPoint;
    public float xRangeLow = 0.25f;
    public float xRangeHigh = 3f;

    public bool paused = false;
    public GameObject pauseUI;

    PlayerController player;

    //Spawn randomly within circle
    public float radius = 10f;

    private void Awake()
    {
        spawnCools = timeBetweenSpawns;

        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (spawnCools > 0) spawnCools -= Time.deltaTime;

        if (spawnCools <= 0)
        {
            SpawnEnemy();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (paused)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
            paused = false;
        }
        else
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
    }

    /*IEnumerator enemyWave()
    {


        yield return new WaitForSeconds(1f);
    }*/

    void SpawnEnemy()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        bool oov = false;
        while (oov == false)
        {
            int xx = Random.Range(0, 4);
            //int xx = 0;
            if (xx == 0)
            {
                //Spawns above player
                //Circle spawn point
                spawnPoint = Random.insideUnitCircle * radius;
                if (spawnPoint.y > bounds.y + 0.25f)
                {
                    oov = true;
                    break;
                }
                else
                {
                    xx += Random.Range(1, 4);
                    xx %= 4;
                    spawnPoint = Random.insideUnitCircle * radius;
                }
                //spawnPoint = new Vector3(Random.Range(-bounds.x + 1f, bounds.x - 1f), bounds.y + Random.Range(0.25f, 3f), 0f);
            }
            else if (xx == 1)
            {
                //Spawns below player
                //spawnPoint = new Vector3(Random.Range(-bounds.x + 1f, bounds.x - 1f), -bounds.y - Random.Range(0.25f, 3f), 0f);
                //Circle spawn point
                spawnPoint = Random.insideUnitCircle * radius;
                if (spawnPoint.y < -bounds.y - 0.25f)
                {
                    oov = true;
                    break;
                }
                else
                {
                    xx += Random.Range(1, 4);
                    xx %= 4;
                    spawnPoint = Random.insideUnitCircle * radius;
                }
            }
            else if (xx == 2)
            {
                //Spawns to the right of the player
                //spawnPoint = new Vector3(bounds.x + Random.Range(xRangeLow, xRangeHigh), Random.Range(-bounds.y + 1f, bounds.y - 1f), 0f);
                //Circle spawn point
                spawnPoint = Random.insideUnitCircle * radius;
                if (spawnPoint.x > bounds.x + 0.25f)
                {
                    oov = true;
                    break;
                }
                else
                {
                    xx += Random.Range(1, 4);
                    xx %= 4;
                    spawnPoint = Random.insideUnitCircle * radius;
                }
            }
            else if (xx == 3)
            {
                //Spawns to the left of the player
                //spawnPoint = new Vector3(-bounds.x - Random.Range(xRangeLow, xRangeHigh), Random.Range(-bounds.y + 1f, bounds.y - 1f), 0f);
                //Circle spawn point
                spawnPoint = Random.insideUnitCircle * radius;
                if (spawnPoint.x < -bounds.x - 0.25f)
                {
                    oov = true;
                    break;
                }
                else
                {
                    xx += Random.Range(1, 4);
                    xx %= 4;
                    spawnPoint = Random.insideUnitCircle * radius;
                }
            }
        }

        if (oov)
        {
            //Spawn the enemy
            Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], spawnPoint, Quaternion.identity);
            spawnCools = timeBetweenSpawns;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero, radius);
    }
}
