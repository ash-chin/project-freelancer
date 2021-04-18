using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float tumble;
    public float rotationSpeed;
    Rigidbody rb;
    Transform sphereCenter;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        rotationSpeed = 1f;

    }

    public void SetSphere(Transform center)
    {
        sphereCenter = center;
    }

    public void SetRotationSpeed(float value)
    {
        rotationSpeed = value;

    }


    private void Update()
    {
        transform.RotateAround(sphereCenter.position, Vector3.up, rotationSpeed*Time.deltaTime);

    }
}