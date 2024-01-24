using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    private Animator animator;
    private float MovementTimer = 0f;
    private float MaxTimeForNextMove = 1.5f;
    private playerMovement sn;
    private spawner spawn;
    private BoxCollider2D box;
    private AudioSource explosion;
    private GameObject audioSource;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sn = FindObjectOfType<playerMovement>();
        box = FindObjectOfType<BoxCollider2D>();
        spawn = FindObjectOfType<spawner>();
        audioSource = GameObject.FindGameObjectWithTag("audio");
        explosion = audioSource.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

        // Update is called once per frame
        void Update()
    {
        MovementTimer += Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerAttack"))
        {
            box.enabled = false;
            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            if (gameObject.name == "tripleSlime(Clone)")
            {
                Debug.Log("hi?");
                spawn.spawnSlime(rb.transform.position);
            }
            animator.SetBool("isDead", true);
            explosion.Play(0);
            Destroy(gameObject, 0.25f);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (MovementTimer! > MaxTimeForNextMove)
            {
                sn.gotHit();
                MovementTimer = 0f;
            }
        }
    }
}
