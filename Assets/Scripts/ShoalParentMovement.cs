using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoalParentMovement : MonoBehaviour
{
    // determines how close the parent has to be to the target to redirect
    public float minDistance;
    // determines how fast it turns
    public float turnSpeed;
    // determines forward speed
    public float speed;
    // the list of transform targets
    public Transform[] loop;
    // the point where the shoal parent is on the index
    private int loopIndex;
    // the next target
    private Transform target;
    // the distance from the current target
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        loopIndex = 0;
        target = loop[loopIndex];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // we check the distance between the parent and the target
        distance = Vector3.Distance(transform.position, target.position);
        // if the distance between the target and the parent is less than our specified min distance
        if (distance < minDistance)
        {
            // increment the loop index, using modulo to set it back to the beginning of the list if necessary
            loopIndex = (loopIndex + 1) % loop.Length;
            // update the new target to the 'next' index in the loop list
            target = loop[loopIndex];
        }

        // move the parent forward at speed relative to itself
        transform.Translate(Time.deltaTime * Vector3.forward * speed, Space.Self);

        // now we incrementally rotate the parent
        var rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);


    }
}
