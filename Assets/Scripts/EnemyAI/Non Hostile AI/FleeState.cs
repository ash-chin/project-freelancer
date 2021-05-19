using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : MonoBehaviour
{
    // the player being fleed from
    public Transform player;
    // the distance form the player
    private float playerDistance;
    // the distance at which it return to dithering
    public float maxFleeDistance;
    // The dithering script itself
    public DitherState dithering;

    private void FixedUpdate()
    {
        playerDistance = Vector3.Distance(player.position, this.transform.position);
    }
}
