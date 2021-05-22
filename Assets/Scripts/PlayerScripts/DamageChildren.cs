using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageChildren : MonoBehaviour
{
    public Player_Asset_Manager player;

    private void OnCollisionEnter(Collision collision)
    {
        player.HullDamage();
    }
}
