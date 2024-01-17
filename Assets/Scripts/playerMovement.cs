using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 6f;
    private float jumpingPower = 6f;
    private bool isFacingRight = true;
    public bool isGrounded = true;
    public int jumpAmount = 0;
    public Animator animator;

    private Rigidbody2D rb;
    private AudioSource jump;
    public playerStats sn;
    private float playerDamageDelay = 1f;
    private float playerDamageTimer = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isWalking",false);
        jump = GetComponent<AudioSource>();
        sn = FindObjectOfType<playerStats>();
    }
    void Update()
    {
        playerDamageTimer += Time.deltaTime;
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && jumpAmount < 2)
        {
            jumpAmount++;
            animator.SetBool("isJumping", true);
            if(jumpAmount == 2)
            {
                animator.SetBool("isSecondJumping", true);
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jump.Play(0);
        }
        Flip();
        if(Input.GetKey("d") || Input.GetKey("a")) 
        {
            if(isGrounded == true)
            {
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            jumpAmount = 0;
            isGrounded = true;
            animator.SetBool("isSecondJumping", false);
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            isGrounded = false;
        }
    }

    public void gotHit()
    {
        if (playerDamageTimer! > playerDamageDelay)
        {
            Debug.Log("Player got hit!");
            rb.AddForce(transform.up * 3, ForceMode2D.Impulse);
            playerDamageTimer = 0f;
            sn.getHit();
        }
    }
}
