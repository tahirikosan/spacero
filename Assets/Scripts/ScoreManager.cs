using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    private Text txtScore;

    private int score = 0;

    public void UpdateScore(int amount)
    {
        score += amount;
        txtScore.text = "SCORE: " + score;
    }

    private void Start()
    {
        UpdateScore(0);
    }

}
