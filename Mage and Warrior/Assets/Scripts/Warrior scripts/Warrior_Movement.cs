using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;

    public float health = 3;
    public float damage = 1;

    [SerializeField] private bool isFacingRight = true;

    public Vector2 movement;

    public Camera myCamera;
    public Camera yourCamera;

    public bool isAttacking = false;

    public float direction = 1;

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            MoveCheck();
        }
        
        Attack();

        SetAnimation();
    }

    private void MoveCheck()
    {
        if (Input.GetKey(KeyCode.I))
        {
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.J))
        {
            movement.x = -1;
            isFacingRight = false;
            direction = -1;
        }
        if (Input.GetKey(KeyCode.K))
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.L))
        {
            isFacingRight = true;
            movement.x = 1;
            direction = 1;
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            movement.y = 0;
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            movement.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            movement.y = 0;
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            movement.x = 0;
        }
    }

    private void SetAnimation()
    {
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.magnitude);
        anim.SetBool("isFacingRight", isFacingRight);
        anim.SetFloat("direction", direction);
    }
    
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.U) && isFacingRight == true && isAttacking == false)
        {
            anim.SetTrigger("r_attack");
        } else if (Input.GetKeyDown(KeyCode.U) && isFacingRight == false && isAttacking == false)
        {
            anim.SetTrigger("l_attack");
        }
    }
    
    private void FixedUpdate()
    {
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 10f) * -1;
    }

    public void DeadCheck()
    {
        if (health <= 0)
        {
            Debug.Log("YOU'RE DEAD");
            //Destroy(gameObject);
            SingleScreen();
        }
    }

    private void SingleScreen()
    {
        //teleport
        transform.position = new Vector3(10000, 100000, 100000);

        //change rect size
        myCamera.rect = new Rect(0, 0, 0, 0);
        yourCamera.rect = new Rect(0, 0, 1, 1);
    }
}
