using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoalFleePatrol : MonoBehaviour
{
    // the distance at which the shoal begins fleeing the player
    public float minFleeDistance;
    // how close it needs to be to an objective to turn. I recommend 3-5 depending on size. Do not set to 0
    public float minTurnDistance;
    // the list of patrol points
    public Transform[] patrolPoints;
    // the index in the patrol list
    private int index = 0;
    // the current patrol point
    private Transform target;
    // distance to the target
    private float targetDistance;
    // the speed at which it turns
    public float turnDamping;
    // the speed at which it moves
    public float speed;
    // the distance from the player
    private float playerDistance;
    // the player
    // THIS HAS BEEN SET TO A GENERIC GAME OBJECT SO THAT THE SCRIPT CAN BE APPLIED
    // TO FLEE ANY 'PREDATOR'
    public GameObject player;
    // the flee script to be turned on
    public ShoalFleeFleeing fleeMode;


    private void Start()
    {
        target = patrolPoints[index];
    }
    private void FixedUpdate()
    {
        // determine how far the player is away
        playerDistance = Vector3.Distance(player.transform.position, transform.position);

        // if the player is too close
        if (playerDistance <= minFleeDistance)
        {
            // turn the flee mode script on, and this off
            fleeMode.enabled = true;
            this.enabled = false;
        }

        // determine the distance to the next patrol point
        targetDistance = Vector3.Distance(target.transform.position, this.transform.position);

        // if close enough to the next patrol point
        if (targetDistance <= minTurnDistance)
        {
            // index in a looping fashion using modulo
            index = (index + 1) % patrolPoints.Length;
        }

        this.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

        var rotation = Quaternion.LookRotation(target.position - this.transform.position);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, turnDamping * Time.deltaTime);
    }
}
