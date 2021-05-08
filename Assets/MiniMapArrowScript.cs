using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapArrowScript : MonoBehaviour
{
    public Transform pointingAt;
    private Vector3 _angle;
    private Vector3 direction;
    private Quaternion zeroOut;

    void Start()
    {
        zeroOut = new Quaternion (1.0f, 1.0f, 0.0f, 1.0f);
    }


    void Update()
    {
        transform.LookAt(new Vector3(pointingAt.position.x, transform.position.y, pointingAt.position.z));

        Vector3 eulerRotation = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(90f, 0.0f, eulerRotation.z);

    }
}
