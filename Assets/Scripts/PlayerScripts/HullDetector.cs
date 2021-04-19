using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullDetector : MonoBehaviour
{
    // This class only exists so that it can be attached to the ship,
    // and then call a function on another script object when collision
    // is detected;

    public Player_Space_Ship_Movement parent;

    private void OnTriggerEnter(Collider other)
    {
        parent.HullDamage();
    }
}
