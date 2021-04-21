using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public float rotateSpeed;
    private Vector3 rotation;

    private void Start()
    {
        rotation = new Vector3(1f, 0f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotation * rotateSpeed * Time.deltaTime);
    }
}
