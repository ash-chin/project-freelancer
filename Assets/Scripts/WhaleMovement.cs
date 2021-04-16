using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMovement : MonoBehaviour
{
    public Transform endpoint;
    public float movSpeed;
    public float rotSpeed;
    private Vector3 direction;

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = (endpoint.position - transform.position);
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime * movSpeed);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed);
    }
}
