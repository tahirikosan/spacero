using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    private Text txtScore;
    [SerializeField]
    private Text txtHighScore;

    private int score = 0;
    private int highScore = 0;

    [SerializeField]
    private PlayerController playerController;

    public void UpdateScore(int amount)
    {
        score += amount;
        txtScore.text = "SCORE: " + score;

        if (score % 10 == 0 && score != 0)
        {
            playerController.UpdateLevel();
        }
    }

    public void SetHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGH_SCORE", highScore);
        }
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);
        txtHighScore.text = "HScore: " + highScore;
        UpdateScore(0);
    }

}
