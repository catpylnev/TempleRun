using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleRun_CollectableItem : MonoBehaviour
{
    public int scoreValue = 10;
    public float speedIncrease = 0.25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TempleRun_PlayerController playerController = other.GetComponent<TempleRun_PlayerController>();

            if (playerController != null)
            {
                // Increase score
                playerController.IncreaseScore(scoreValue);

                // Increase speed
                playerController.IncreaseSpeed(speedIncrease);

                // Destroy the collectable item
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("PlayerController is null!");
            }
        }
    }
}
