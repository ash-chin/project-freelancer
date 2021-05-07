using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShoalPatrolParent : MonoBehaviour
{
    // this is the minimum distance the subject has to be from its goal before turning
    public float minDistance;
    // determines turn speed. Try to keep it less than 1;
    public float turnDamping;
    // determines forward speed
    // SET THIS LOWER THAN THE PLAYER'S SPEED, OTHERWISE THEY WILL BE INESCAPABLE
    public float speed;
    // the list of empty game objects that the subject will use as locations to patrol
    public Transform[] patrolLoop;
    // this is the index on the list
    private int loopIndex;
    // distance from the the next patrol target
    private float patrolDistance;
    // the next target in the loop
    private Transform target;
    // distance from the player at any given point
    private float playerDistance;
    // the player object, also useful for invoking damage
    public Player_Space_Ship_Movement player;
    // this is how close the shoal has to be to the player before they start attacking
    public float attackDistance;
    // this is the attack state that the subject will enter when prompted
    public EnemyShoalAttackParent attackMode;

    private void Start()
    {
        // set the target of approach to the beginning of the loop
        loopIndex = 0;
        target = patrolLoop[loopIndex];
        // make sure that the subject starts in patrol mode and not attack mode;
        attackMode.enabled = false;
    }

    private void FixedUpdate()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        // if ths distance between the subject and the player is less than the amount required to initiate attack mode
        // disable this script, and switch to the attack state
        if (playerDistance <= attackDistance)
        {
            // enable the attack mode movement, disable this mode
            attackMode.enabled = true;
            this.enabled = false;
        }
        else
        {
            // measure the distance to the next stage of the patrol loop
            patrolDistance = Vector3.Distance(transform.position, target.position);

            // if the subject has come close enough to it
            if (patrolDistance <= minDistance)
            {
                // loop to the next index in the patrol, going in a circular fashion
                loopIndex = (loopIndex + 1) % patrolLoop.Length;
                target = patrolLoop[loopIndex];
            }

            // move the subject forward by the given speed. This is agnostic of the direction to the target
            // by keeping forward movement agnostic of the direction to the target we get this nice smooth
            // swimming motion that turns naturally as patrol points are reached.
            transform.Translate(Time.deltaTime * Vector3.forward * speed);

            // find the rotation to the current target
            var rotation = Quaternion.LookRotation(target.position - transform.position);
            // rotate to face it at the given speed if not already facing it.
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnDamping * Time.deltaTime);
        }
    }
}
