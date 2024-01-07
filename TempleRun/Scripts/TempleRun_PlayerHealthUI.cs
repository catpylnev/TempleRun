using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempleRun_PlayerHealthUI : MonoBehaviour
{
    public TempleRun_PlayerController playerController; 
    public Text healthText;
    public Text scoreText;

    void Start()
    {
        healthText = GameObject.Find("HealthText").GetComponent<Text>(); 
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();   

        UpdateHealthText();
    }

    void Update()
    {
        UpdateHealthText();
    }

    void UpdateHealthText()
    {
        if (playerController != null)
        {
            healthText.text = "Health: " + playerController.health;
            scoreText.text = "Score: " + playerController.score; // Update the score text as well
        }
    }
}
