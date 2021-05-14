using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullDetector : MonoBehaviour
{
    // This class only exists so that it can be attached to the ship,
    // and then call a function on another script object when collision
    // is detected;

    public Player_Asset_Manager manager;

    private void OnTriggerEnter(Collider other)
    {
        manager.HullDamage();
    }
}
