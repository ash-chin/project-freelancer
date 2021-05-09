using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageChildren : MonoBehaviour
{
    public Player_Space_Ship_Movement player;

    private void OnCollisionEnter(Collision collision)
    {
        player.HullDamage();
    }
}
