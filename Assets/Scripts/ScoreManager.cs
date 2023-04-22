using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    private Text txtScore;

    private int score = 0;

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

    private void Start()
    {
        UpdateScore(0);
    }

}
