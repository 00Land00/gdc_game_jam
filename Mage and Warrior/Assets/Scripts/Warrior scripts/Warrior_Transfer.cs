using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Transfer : MonoBehaviour
{
    public Warrior_Movement w_m;

    //used for Animation events
    public void AttackTrue()
    {
        w_m.movement.x = 0;
        w_m.movement.y = 0;
        w_m.isAttacking = true;
    }

    public void AttackFalse()
    {
        w_m.movement.x = 0;
        w_m.movement.y = 0;
        w_m.isAttacking = false;
    }
}
