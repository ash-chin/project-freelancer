using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapArrowScript : MonoBehaviour
{
    public Transform pointingAt;
    public Transform playerPos;

    private float sinVal;

    void Start()
    {
        sinVal = 0;
    }


    void Update()
    {
        Rotate();

        ArrowAnimation();

        FollowPlayer();
    }

    void Rotate()
    {

        transform.LookAt(new Vector3(pointingAt.position.x, pointingAt.position.y, pointingAt.position.z));

        Vector3 eulerRotation = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(90f, eulerRotation.y, eulerRotation.z);
    }

    void ArrowAnimation()
    {
        sinVal += 0.03f;

        if (sinVal >= 2 * Mathf.PI)
            sinVal = 0;

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Mathf.Sin(sinVal) * 2, transform.localScale.z);
    }

    void FollowPlayer()
    {
        Vector3 newPosition = playerPos.position;

        newPosition.z += 400;

        newPosition.y = transform.position.y;

        transform.position = newPosition;
    }
}
