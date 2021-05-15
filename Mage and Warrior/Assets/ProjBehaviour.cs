using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjBehaviour : MonoBehaviour
{
    public GameObject caster;
    public int dy = 0;
    public int dx = 1;
    public float moveSpeed = 4f;
    int damageValue = 1;

    Rigidbody2D rb;
    //degrees per second??
    float rotateSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        //normally would set the correct x and y values to start from here, but instantiate will take care of it i think   
        rb = this.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Quaternion.Euler(rotateSpeed*Time.fixedDeltaTime,0,0);
        Vector2 movement = new Vector2(dx, dy);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            //this wont work if we have more enemy types
            //print("deal damage?");
            collision.gameObject.GetComponent<Movement>().health -= damageValue;
            collision.gameObject.GetComponent<Movement>().DeadCheck();
        }
        Destroy(this.gameObject);
    }
}
