using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSphereMovementNoTurn : MonoBehaviour
{
    // the speed the object moves at.
    public float speed;
    // this float is the radius of the sphere
    public float radius;
    // this is the center of the sphere
    public Transform center;

    // these are the x, y, z position integer conversions. Will make sense in contenxt
    private int xMin, xMax, yMin, yMax, zMin, zMax;

    // minimum distance before moving away
    public float minDist;
    // the measure of distance
    private float distance;

    // the target the object is moving towards
    public Transform target;
    // this will be used to update the target position
    private Vector3 pos;


    private void SetNewTarget()
    {
        // find the minimum and maximum possible x positions, convert to integers because that's what
        // the fuuuuuuuuuuuuucking randomg number generator excepts
        xMin = (int)( center.position.x - radius);
        xMax = (int)(center.position.x + radius);
        // set the new position x equal to a type converted randomly generated number;
        pos.x = (float)Random.Range(xMin, xMax);

        // repeat for other axies
        yMin = (int)(center.position.y - radius);
        yMax = (int)(center.position.y + radius);

        pos.y = (float)Random.Range(yMin, yMax);

        zMin = (int)(center.position.z - radius);
        zMax = (int)(center.position.z + radius);

        pos.z = (float)Random.Range(zMin, zMax);

        // update the target's position
        target.position = pos;

        // measure its distance from the center
        distance = Vector3.Distance(center.position, target.position);


        // if it is further away from the center than the radius, then it exists somewhere in the radius cube,
        // not the radius sphere
        if (distance > radius)
        {
            // oops try again;
            SetNewTarget();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        // measure the distance between the object and its target
        if (target == null)
        {
            Debug.LogError("The target should exist");
        }

        distance = Vector3.Distance(transform.position, target.position);

        // if the target is close enough, get it a new target
        if (distance < minDist)
        {
            SetNewTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }
}
