using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    private Animator animator;
    private float MovementTimer = 0f;
    private float MaxTimeForNextMove = 1.5f;
    public playerMovement sn;
    private BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sn = FindObjectOfType<playerMovement>();
        box = FindObjectOfType<BoxCollider2D>();
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
            animator.SetBool("isDead", true);
            Destroy(gameObject, 0.5f);
            if (gameObject.name == "tripleSlime")
            {
            }
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
