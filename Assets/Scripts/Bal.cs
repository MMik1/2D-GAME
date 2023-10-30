using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float SpeedIncrease = 0.25f;
    [SerializeField] private Text playerScore;
    [SerializeField] private Text AIScore;
    [SerializeField] private Text endGameText;

    private int hitCounter;
    private Rigidbody2D rb;
    private int playerPoints = 0; // Add this line
    private int aiPoints = 0; // Add this line
    private bool gameEnded = false; // Add this line

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }
    private void FixedUpdate()
    {
        if (!gameEnded)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (SpeedIncrease * hitCounter));
        }
        else
        {
            // Freeze the ball by setting its velocity to zero
            rb.velocity = Vector2.zero;
        }
    }
    private void StartBall()
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + SpeedIncrease * hitCounter);
    }

    private void ResetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection, yDirection;
        if (transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }
        yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed * hitCounter);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player" || collision.gameObject.name == "AI")
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            ResetBall();
            playerPoints++;
            playerScore.text = playerPoints.ToString();
        }
        else if (transform.position.x < 0)
        {
            ResetBall();
            aiPoints++;
            AIScore.text = aiPoints.ToString();
        }

        if (playerPoints >= 5 || aiPoints >= 5)
        {
            gameEnded = true;

            if (playerPoints > aiPoints)
            {
                endGameText.text = "Player one Wins!";
            }
            else
            {
                endGameText.text = "Player two Wins!";
            }

            endGameText.enabled = true;
            playerScore.enabled = false;
            AIScore.enabled = false;
        }

        if (!gameEnded)
        {
            ResetBall();
        }
    }
}