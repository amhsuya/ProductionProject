using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public int highScore = 0;

    public static GameManager instance;

    public Text scoreText;
    public Text highScoreText;

    private const string HighScoreKey = "highscore";

    public void IncrementScore()
    {
        
        score++;
        scoreText.text = "SCORE: " + score.ToString();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            highScoreText.text = "HIGHSCORE: " + highScore.ToString();
        }
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
}
