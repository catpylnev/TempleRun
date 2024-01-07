using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempleRun_PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float verticalPositionStep = 1f;
    public float jumpHeight = 0.5f;
    public float jumpDuration = 0.25f;
    public int health = 3;
    public int score = 0;
    public AudioSource hurtSound;

    public Text scoreText;

    private float[] verticalPositions = { 1.5f, 0f, -1.5f };
    private int currentVerticalIndex = 1;
    private bool isJumping = false;
    private Animator playerAnimator;

    void Start()
    {
        // Get the Animator component on the player
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleHorizontalMovement();

        if (!isJumping)
        {
            HandleVerticalMovement();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Jump());
                playerAnimator.SetBool("JumpAnima", true);
            }
        }
 
    }

    void HandleHorizontalMovement()
    {
        // Constantly move the player to the right
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void HandleVerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveToNextVerticalPosition();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveToPreviousVerticalPosition();
        }
    }

    void MoveToNextVerticalPosition()
    {
        if (currentVerticalIndex < verticalPositions.Length - 1)
        {
            currentVerticalIndex++;
            UpdateVerticalPosition();
        }
    }

    void MoveToPreviousVerticalPosition()
    {
        if (currentVerticalIndex > 0)
        {
            currentVerticalIndex--;
            UpdateVerticalPosition();
        }
    }

    void UpdateVerticalPosition()
    {
        float targetVerticalPosition = verticalPositions[currentVerticalIndex];
        transform.position = new Vector3(transform.position.x, targetVerticalPosition, transform.position.z);
    }

    IEnumerator Jump()
    {
        isJumping = true;

        float initialY = transform.position.y;
        float jumpEndTime = Time.time + jumpDuration;

        while (Time.time < jumpEndTime)
        {
            float t = 1 - Mathf.Pow((jumpEndTime - Time.time) / jumpDuration, 2);
            transform.Translate(Vector3.up * Mathf.Lerp(0, jumpHeight, t) * Time.deltaTime);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, initialY + jumpHeight, transform.position.z);

        yield return new WaitForSeconds(0.5f);

        while (Time.time < jumpEndTime + jumpDuration)
        {
            float t = Mathf.Pow((Time.time - jumpEndTime) / jumpDuration, 2);
            transform.Translate(Vector3.down * Mathf.Lerp(0, jumpHeight, t) * Time.deltaTime);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, initialY, transform.position.z);

        isJumping = false;
        playerAnimator.SetBool("JumpAnima", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) //enemy is spikes and enemy
        {
            LoseHealth();
            Debug.Log("LoseHealth triggered!");
        }
        else if (other.CompareTag("Wall") && !isJumping) //Wall is my hole obstacle
        {
            LoseHealth();
            Debug.Log("LoseHealth triggered!");
        }
        else if (other.CompareTag("Collectable"))
        {
            IncreaseScore(10);
            IncreaseSpeed(0.25f); // might change speed increase
            Destroy(other.gameObject);
        }
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    void LoseHealth()
    {
        health--;

        if (health <= 0)
        {
            PlayerPrefs.SetInt("FinalScore", score);
            SceneManager.LoadScene("TempleRunGameOver");
        }
        else
        {
            // Play hurt sound
            if (hurtSound != null)
            {
                hurtSound.PlayOneShot(hurtSound.clip);
            }
        }
    }
}
