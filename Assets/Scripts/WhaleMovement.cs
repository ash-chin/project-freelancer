using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMovement : MonoBehaviour
{
    public float damping;
    public float speed;
    public Transform[] loop;
    private int loopIndex;
    private Transform target;
    private float distance;


    private void Start()
    {
        loopIndex = 0;
        target = loop[loopIndex];
    }
  
    void FixedUpdate()
    {
        distance = Vector3.Distance(target.position, transform.position);
        if (distance < 10)
        {
            loopIndex = (loopIndex + 1) % loop.Length;
            target = loop[loopIndex];
        }

        transform.Translate(Time.deltaTime * Vector3.back * speed, Space.Self);

        var currentForward = transform.forward;
        var lookpos = target.position - transform.position;

        var rotation = Quaternion.LookRotation(-lookpos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
