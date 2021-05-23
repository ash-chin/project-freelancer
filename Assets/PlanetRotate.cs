using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    public Transform rotateAround;
    public float planetRotateSpeed;
    public float orbitSpeed;


    // Update is called once per frame
    void Update()
    {
        
        transform.RotateAround(rotateAround.position, rotateAround.up, orbitSpeed * Time.deltaTime);
        transform.Rotate(transform.up * planetRotateSpeed * Time.deltaTime);
    }
}
