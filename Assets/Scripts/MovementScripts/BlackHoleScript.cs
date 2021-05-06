using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public Player_Space_Ship_Movement player;
    public float maxDistance;
    private float distance;

    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= maxDistance)
        {

        }
    }
}
