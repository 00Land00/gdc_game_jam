using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_MOvement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    public float health = 3;
    public int prev_y = 0;
    public int prev_x = 0;

    Vector2 movement;
    private bool isFacingRight = true;

    public Camera myCamera;
    public Camera yourCamera;
    int attackType = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (isFacingRight == true)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                isFacingRight = false;
            }
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //print("flipping" + isFacingRight);
            if (isFacingRight == false)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                isFacingRight = true;
            }
            movement.x = 1;
        }
        if(Input.GetKey(KeyCode.Q))
        {
            attackType = 1;
            anim.SetBool("Attacking", true);
        }
        /*
        if(Input.GetKey(KeyCode.E))
        {
            attackType = 2;
            anim.SetBool("Attacking", true);
        }
        */




        if (Input.GetKeyUp(KeyCode.W))
        {
            prev_y = 1;
            movement.y = 0;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            prev_x = -1;
            movement.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            prev_y = -1;
            movement.y = 0;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            prev_x = 1;
            movement.x = 0;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            anim.SetBool("Attacking", false);
        }
        /*
        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool("Attacking", false);
        }
        */
        anim.SetFloat("Speed", movement.magnitude);
    }

    private void FixedUpdate()
    {
        movement.Normalize();
        if (attackType == 0)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 10f) * -1;
    }


    public void attack()
    {
        //print("fired a projectile");
        if (attackType == 1)
        {
            //print("shootin");
            SpawnThings sn = GameObject.Find("spawner").GetComponent<SpawnThings>();
            int i;
            if (isFacingRight)
            {
                i = sn.SpawnDamageProjectile(0.5f, 0);
            }
            else
            {
                i = sn.SpawnDamageProjectile(-0.5f, 0);

            }

            //print("Setting the x and y of clone "+ i+ "to be (" +prev_x+ ", " +prev_y+ ")");
            sn.Clone[i].GetComponent<ProjBehaviour>().dy = prev_y;
            sn.Clone[i].GetComponent<ProjBehaviour>().dx = prev_x;

        }
        /*
        if (attackType == 2)
        {
            //print("shootin");
            SpawnThings sn = GameObject.Find("spawner").GetComponent<SpawnThings>();
            int i = sn.SpawnRockProjectile(0, 0);

            sn.Clone[i].GetComponent<ProjBehaviour>().dy = prev_y;
            sn.Clone[i].GetComponent<ProjBehaviour>().dx = prev_x;
        }
        */
    }
    public void resetAttack()
    {
        //print("finished an attack");
        attackType = 0;
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
        Debug.Log("going singlescreen");
        //teleport
        rb.gameObject.transform.position = new Vector3(10000, 100000, 100000);


        //change rect size
        myCamera.rect = new Rect(0, 0, 0, 0);
        yourCamera.rect = new Rect(0, 0, 1, 1);
    }
}
