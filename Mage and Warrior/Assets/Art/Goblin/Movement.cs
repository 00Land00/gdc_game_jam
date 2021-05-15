using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //movement / range
    public float moveSpeed = 4f;
    public float attackRange = 6f;
    public float aggroRange = 200f;

    //enemy specific
    public float health = 3f;
    public int damage = 1;
    public Animator anim;
    public Rigidbody2D rb;
    bool facingRight = true;
    [SerializeField] private Rigidbody2D currentTarget;

    //player specific
    Rigidbody2D magerb;
    Rigidbody2D warriorrb;
    float mageDistance = 10000;
    float warriorDistance = 10000;

    
    float findDistance(float x1,float y1, float x2, float y2)
    {
        float dx = x2 - x1;
        float dy = y2 - y1;
        return Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));
    }

    private void Start()
    {
        magerb = GameObject.Find("Wizard").GetComponent<Rigidbody2D>();
        warriorrb = GameObject.Find("Warrior").GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        float currX = rb.position.x;
        float currY = rb.position.y;
        //consider setting to 10mil if either is dead
        float mageX = magerb.position.x;
        float mageY = magerb.position.y;
        float warriorX = warriorrb.position.x;
        float warriorY = warriorrb.position.y;

        //get distance to each player, so we can use them in fixed update
        mageDistance = findDistance(currX, currY, mageX, mageY);
        warriorDistance = findDistance(currX, currY, warriorX, warriorY);
    }

    private void FixedUpdate()
    {
        //attack if in range
        if (mageDistance <= attackRange || warriorDistance <= attackRange)
        {
            anim.SetBool("Attacking", true);
        }
        else if ((mageDistance < aggroRange || warriorDistance < aggroRange))
        {
            anim.SetBool("Attacking", false);
            //go for the closer player.
            if (mageDistance <= warriorDistance)
            {
                //moving
                transform.position = Vector2.MoveTowards(rb.position, magerb.position, moveSpeed * Time.deltaTime);
                currentTarget = magerb;
                if (magerb.position.x < rb.position.x && facingRight == true)
                {
                    facingRight = false;
                    //print("Switching Sides");
                    // Multiply the player's x local scale by -1
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
                else if (magerb.position.x > rb.position.x && facingRight == false)
                {
                    facingRight = true;
                    //print("Switching Sides");
                    // Multiply the player's x local scale by -1
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
                anim.SetFloat("Speed", 1);
            }
            else
            {
                //moving
                transform.position = Vector2.MoveTowards(rb.position, warriorrb.position, moveSpeed * Time.deltaTime);
                currentTarget = warriorrb;
                if (warriorrb.position.x < rb.position.x && facingRight == true)
                {
                    facingRight = false;
                    print("Switching Sides");
                    // Multiply the player's x local scale by -1
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
                else if (warriorrb.position.x > rb.position.x && facingRight == false)
                {
                    facingRight = true;
                    print("Switching Sides");
                    // Multiply the player's x local scale by -1
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
                anim.SetFloat("Speed", 1);
            }
        }
        else
        {
            anim.SetBool("Attacking", false);
            //not moving
            anim.SetFloat("Speed", 0);
        }
    this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 10f) * -1;
    }

    private void DamagePlayer(int damageValue, Rigidbody2D rb)
    {
        /*
        rb is the rigidbody of the current target of enemy. either wizard or mage
        get the movement script which contains the health value of either player from rb
        deduct that value by damageValue
        call their deadcheck to see if they die
        */
        if(rb == magerb)
        {
            Wizard_MOvement w_m = rb.gameObject.GetComponentInChildren<Wizard_MOvement>();
            w_m.health -= damageValue;
            w_m.DeadCheck();
        } else if(rb == warriorrb)
        {
            Warrior_Movement w_m = rb.gameObject.GetComponent<Warrior_Movement>();
            w_m.health -= damageValue;
            w_m.DeadCheck();
        }
    }

    public void StabPlayer()
    {
        DamagePlayer(damage, currentTarget);
    }

    public void DeadCheck()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        
        }
    }
}
