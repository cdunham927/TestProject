using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BreakoutGameController : MonoBehaviour
{
    public int lives = 3;
    public GameObject deadUI;
    public Text lifeText;
    public Text brickText;
    int numBricks;
    bool dead;

    private void Awake()
    {
        lifeText.text = "Lives: " + lives.ToString();
        numBricks = GameObject.FindGameObjectsWithTag("Wall").Length;
        brickText.text = "Bricks left: " + numBricks.ToString();
        dead = false;
    }

    public void HitBrick()
    {
        numBricks--;
        brickText.text = "Bricks left: " + numBricks.ToString();

        if (numBricks <= 0)
        {
            Invoke("Restart", 2f);
        }
    }

    public void LoseHealth()
    {
        lives--;
        lifeText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if (dead)
        {
            if (Input.anyKeyDown)
            {
                Restart();
            }
        }
    }

    void Die()
    {
        deadUI.SetActive(true);
        dead = true;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
