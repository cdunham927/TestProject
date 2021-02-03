using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PongGameController : MonoBehaviour
{
    public int p1Score;
    public int p2Score;
    public Text scoreText;

    public int scoreToWin;

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.P)) Score(true);
        }
    }

    public void Score(bool p1)
    {
        if (p1)
        {
            p1Score++;
            if (p1Score >= scoreToWin)
            {
                //Player 1 wins
                //Debug.Log("Player 1 wins");
            }
        }
        else
        {
            p2Score++;
            if (p2Score >= scoreToWin)
            {
                //Player 2 wins
                //Debug.Log("Player 2 wins");
            }
        }

        scoreText.text = p1Score.ToString() + " : " + p2Score.ToString();

        //scoreText.text += "/nPlayer 1 wins";
    }
}
