using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : MonoBehaviour
{
    // the player in question
    public Transform player;
    // distance to the player
    private float playerDistance;
    // the distance at which the object transfers to the dither state
    public float minDitherDistance;
    // the dither state itself
    public DitherState dithering;
    // the speed of the object
    public float speed;
    // how fast it turns
    public float turnDamping;

    private void Start()
    {
        dithering.enabled = false;
    }

    private void FixedUpdate()
    {
        playerDistance = Vector3.Distance(player.position, this.transform.position);

        if (playerDistance <= minDitherDistance)
        {
            dithering.enabled = true;
            this.enabled = false;
        }

        transform.Translate(Time.deltaTime * Vector3.forward * speed, Space.Self);

        var rotation = Quaternion.LookRotation(player.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, turnDamping * Time.deltaTime);
    }
}
