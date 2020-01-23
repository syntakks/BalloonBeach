using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Score : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0; 
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI highScoreUI;

    private void Start()
    {
        UpdateScore();
        SetupHighScore(); 
    }

    private void SetupHighScore()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        highScoreUI.text = highScore.ToString(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "triangle")
        {
            // Player lose
        }

        if (other.gameObject.tag == "score")
        {
            score++;
            UpdateScore();
            UpdateHighScore();
            Debug.Log("Scored!"); 
        }
    }

    private void UpdateScore()
    {
        scoreUI.text = score.ToString();
    }

    private void UpdateHighScore()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            highScoreUI.text = score.ToString(); 
        }
    }
}
