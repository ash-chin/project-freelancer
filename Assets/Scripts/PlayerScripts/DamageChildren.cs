using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageChildren : MonoBehaviour
{
    // public Player_Asset_Manager player;
    public static GameObject AM;

    void Start()
    {
        if (AM == null)
        {
            AM = GameObject.Find("AssetManager");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // player.HullDamage();
        AM.GetComponent<Player_Asset_Manager>().HullDamage();
    }
}
