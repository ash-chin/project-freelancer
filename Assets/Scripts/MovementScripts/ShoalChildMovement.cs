using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoalChildMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject parent;

    public float speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        // push the object relatively forward in space at the speed of the parent
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

        // match the child rotation identically to the parent rotation.
        transform.rotation = parent.transform.rotation;
    }
}
