using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RacingGameController : MonoBehaviour
{
    public int laps;
    public Text winText;
    public Text countdown;
    public float timeToStart = 3f;
    public bool started = false;
    public float restartTime;

    private void Update()
    {
        if (timeToStart > 0)
        {
            timeToStart -= Time.deltaTime;
            countdown.text = Mathf.RoundToInt(timeToStart).ToString();
        }
        else
        {
            started = true;
            countdown.gameObject.SetActive(false);
        }
    }

    public void EndGame(int num)
    {
        Invoke("Restart", 3f);
        winText.gameObject.SetActive(true);
        winText.text = "Player " + num + " wins!";
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
