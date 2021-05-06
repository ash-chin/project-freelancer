using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationRotator : MonoBehaviour
{
    public float zVal;

    private Vector3 rotationVector;

    private void Start()
    {
        rotationVector = new Vector3(0, 0, zVal);
    }

    private void Update()
    {
        transform.Rotate(rotationVector);
    }
}
