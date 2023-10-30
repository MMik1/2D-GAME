using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isAI;
    [SerializeField] private GameObject ball;

    private Rigidbody2D rb;
    private Vector2 playerMove;

    public AudioSource audioPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isAI)
        {
            AiControl();
        }
        else
        {
            PlayerControl();
        }
    }

    private void PlayerControl()
    {
        playerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    private void AiControl()
    {
        if (ball.transform.position.y > transform.position.y + 0.5f)
        {
            playerMove = new Vector2(0, 1);
        }
        else if (ball.transform.position.y < transform.position.y - 0.5f)
        {
            playerMove = new Vector2(0, -1);
        }
        else
        {
            playerMove = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = playerMove * movementSpeed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CollisionTag")
        {
            audioPlayer.Play();
        }
    }
}
