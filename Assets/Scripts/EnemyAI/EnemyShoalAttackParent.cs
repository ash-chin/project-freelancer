using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoalAttackParent : MonoBehaviour
{
    // this is the timer for queueing new damage to the player
    private float timer;
    // this sets how often it will be performed
    public float damageInterval;
    // how close the player needs to be to receive damage
    public float damageDistance;
    // this determines how much damage will be dealt to the player
    public float damage;
    // this is the player, which will be referenced in several ways
    public Player_Space_Ship_Movement player;
    // this is the distance from the subject to the player
    private float enemyDistance;
    // this is the resting patrol state that the subject will return to when conditions are met
    public EnemyShoalPatrolParent patrol;
    // this is the distance the subject needs to be from the target to break patrol
    // note that it NEEDS TO BE IDENTICAL TO THE DISTANCE DETERMINING ENGAGEMENT
    public float breakEnagementDistance;
    // the speed of the subject
    public float speed;
    // the turn damping of the subject
    public float turnDamping;


    private void FixedUpdate()
    {
        // increment the timer
        timer += Time.deltaTime;
        // calculate the distance between the subject and the target
        enemyDistance = Vector3.Distance(transform.position, player.transform.position);

        // if the enemy is far enough away, break engagement
        if (enemyDistance >= breakEnagementDistance)
        {
            // enable patrol, disable attack state
            patrol.enabled = true;
            this.enabled = false;
        }

        // if the subject is close enough to the player to deal damage
        if (enemyDistance <= damageDistance && timer >= damageInterval)
        {
            // reset the timer
            timer = 0;
            // damage the player
            player.VariableDamage(damage);
        }

        // move the subject forward relative to itself. 
        transform.Translate(Time.deltaTime * speed * Vector3.forward);

        // find the rotation to face the enemy
        var rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        // rotate by interpolation to face the enemy
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnDamping * Time.deltaTime);
    }
}
