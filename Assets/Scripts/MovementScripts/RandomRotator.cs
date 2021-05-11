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
        transform.RotateAround(transform.position, Vector3.up, (0.5f*rotationSpeed)*Time.deltaTime);
        transform.RotateAround(sphereCenter.position, Vector3.down, rotationSpeed * Time.deltaTime);
    }
}