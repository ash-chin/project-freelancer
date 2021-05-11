using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotatorNoCenter : MonoBehaviour
{
    [SerializeField]
    private float tumble;
    public float rotationSpeed;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;


    }


    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, (0.5f * rotationSpeed) * Time.deltaTime);
    }
}
