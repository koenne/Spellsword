using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public Animator animator;
    Collider2D coll;
    private float MovementTimer = 0f;
    private float MaxTimeForNextMove = 0.4f;
    private bool hasAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovementTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            hasAttacked = true;
            animator.SetBool("isAttacking", true);
            coll.enabled = true;
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
        if(MovementTimer !> MaxTimeForNextMove && hasAttacked)
        {
            hasAttacked = false;
            MovementTimer = 0f;
            coll.enabled = false;
        }
    }
}
