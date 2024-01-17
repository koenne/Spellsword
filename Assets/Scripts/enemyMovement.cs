using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{

    private float moveSpeed = 7f;
    private Transform player;
    private GameObject playerObject;
    private Rigidbody2D rb;
    private Vector3 targetPosition;
    private float jumpHeight = 6f;
    private bool isGrounded;
    private bool bigJump = false;
    private float distance;
    private bool doneFlipPositive = false;
    private bool doneFlipNegative = false;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(player.position.x, rb.position.y);
        rb.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        distance = Vector3.Distance(player.transform.position, rb.transform.position);

        jump();
        if (player.position.x > rb.position.x)
        {
            FlipNegative();
        }
        else if(player.position.x < rb.position.x)
        {
            FlipPositive();
        }
    }
    void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("slimeJump"))
        {
            bigJump = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("slimeJump"))
        {
            bigJump = false;
        }
    }
    private void FlipPositive()
    {
        if(doneFlipPositive == false)
        {
            doneFlipPositive = true;
            doneFlipNegative = false;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void FlipNegative()
    {
        if (doneFlipNegative == false)
        {
            doneFlipNegative = true;
            doneFlipPositive = false;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    void jump()
    {
        if (player.position.y > targetPosition.y + 2.5 && isGrounded && bigJump && distance < 8)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        else if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 1);
        }
    }
}
