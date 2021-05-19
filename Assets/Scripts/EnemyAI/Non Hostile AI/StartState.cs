using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : MonoBehaviour
{
    public GameObject[] startLoop;
    private int index;
    public Transform player;
    public float speed;
    public float rotationDamping;
    private Transform target;

    // the follow state, and the distance at which the object transfers to it.
    public FollowState followState;
    public float minFollowDistance;
    public float minTurnDistance;
    private float targetDistance;
    private float playerDistance;

    private void Start()
    {
        index = 0;
        followState.enabled = false;
        target = startLoop[index].transform;
    }

    private void FixedUpdate()
    {
        targetDistance = Vector3.Distance(target.position, this.transform.position);

        if (targetDistance <= minTurnDistance)
        {
            index = (index + 1) % startLoop.Length;
            target = startLoop[index].transform;
        }

        playerDistance = Vector3.Distance(player.position, this.transform.position);

        if (playerDistance <= minFollowDistance)
        {
            followState.enabled = true;
            this.enabled = false;
        }

        this.transform.Translate(Time.deltaTime * Vector3.forward * speed, Space.Self);

        var rotation = Quaternion.LookRotation(target.position - this.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationDamping * Time.deltaTime);
    }
}
