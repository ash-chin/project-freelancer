using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoalChild : MonoBehaviour
{
    // this is the parent that the game object will inherit its transform from
    public GameObject parent;
    // this is the speed of the child
    private float speed;
    // the speed will be inherited from the patrol object
    public EnemyShoalPatrolParent speedParent;
    // the object for inheriting attack damage
    public EnemyShoalAttackParent attackParent;
    // this is the player, which the target will attack
    // it's inherited from the parent
    private Player_Space_Ship_Movement player;
    // the damage distance
    private float damageDistance;
    // the damage dealt
    private float damage;
    // the distance from the target
    private float distance;
    // the timer on damage;
    private float timer;
    // the interval of the timer
    private float attackInterval;

    private void Start()
    {
        damage = attackParent.damage;
        damageDistance = attackParent.damageDistance;
        speed = speedParent.speed;
        attackInterval = attackParent.damageInterval;
        timer = 0;
    }


    private void FixedUpdate()
    {
        // increment the timer by the amount of time that has passed
        timer += Time.deltaTime;

        // check the distance to the player
        distance = Vector3.Distance(player.transform.position, transform.position);

        // if the player is in attack range, and sufficient time has passed
        if (distance <= damageDistance && timer >= attackInterval)
        {
            timer = 0;
            player.VariableDamage(damage);
        }

        // move the subject directly forward
        transform.Translate(Time.deltaTime * speed * Vector3.forward, Space.Self);
        // and match the rotation of the child to the rotation of the parent.
        // we use the game object as a reference to avoid the issue that we can have when the
        // parent switches between modes
        transform.rotation = parent.transform.rotation;
    }
}
