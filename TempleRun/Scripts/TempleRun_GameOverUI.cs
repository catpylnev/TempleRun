using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempleRun_GameOverUI : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        DisplayFinalScore();
    }

    void DisplayFinalScore()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        finalScoreText.text = "Final Score: " + finalScore.ToString();

        Debug.Log("Final Score: " + finalScore);
    }
}
