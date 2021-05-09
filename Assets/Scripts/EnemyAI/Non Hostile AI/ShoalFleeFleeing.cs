using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoalFleeFleeing : MonoBehaviour
{
    // the player. A generic object so it can be replaced with any other creature
    public GameObject predator;
    // the distance it has to be away to return to patrol mode
    public float maxChaseDistance;
    // distance from the predator
    private float predatorDistance;
    // the speed of the subject
    public float speed;
    // the turn speed of the subject
    public float turnDamping;
    // the script this alternates with
    public ShoalFleePatrol patrolMethod;

    private void FixedUpdate()
    {
        predatorDistance = Vector3.Distance(predator.transform.position, transform.position);

        if (predatorDistance >= maxChaseDistance)
        {
            patrolMethod.enabled = true;
            this.enabled = false;
        }

        var rotation = Quaternion.LookRotation(this.transform.position - predator.transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, turnDamping * Time.deltaTime);
    }
}
