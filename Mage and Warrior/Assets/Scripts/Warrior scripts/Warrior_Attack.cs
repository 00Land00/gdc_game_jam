using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Attack : MonoBehaviour
{
    public Warrior_Movement warrior;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Movement enemy = collision.gameObject.GetComponent<Movement>();
            enemy.health -= warrior.damage;
            enemy.DeadCheck();
        }
    }
}
