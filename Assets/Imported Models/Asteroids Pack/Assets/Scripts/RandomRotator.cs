using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
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

    public void SetSphere(Transform center)
    {
    }

    public void SetRotationSpeed(float value)
    {
        rotationSpeed = value;

    }


    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, rotationSpeed*Time.deltaTime);

    }
}