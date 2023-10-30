using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private int playerNumber; 
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
        if (playerNumber == 1)
        {
            PlayerControlPlayer1();
        }
        else if (playerNumber == 2)
        {
            PlayerControlPlayer2();
        }
    }

    private void PlayerControlPlayer1()
    {
        playerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    private void PlayerControlPlayer2()
    {
        playerMove = new Vector2(0, Input.GetAxisRaw("Vertical2")); 
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