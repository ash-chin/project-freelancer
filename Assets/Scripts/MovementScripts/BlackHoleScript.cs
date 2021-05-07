using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlackHoleScript : MonoBehaviour
{
    public Player_Space_Ship_Movement player;
    public float maxDistance;
    private float distance;
    private Vector3 relativePosition;
    private CharacterController controller;
    private float speed;

    private void Start()
    {
        controller = player.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= maxDistance)
        {
            if (distance < 500)
            {
                player.VariableDamage((500 / distance) * Time.deltaTime);
            }


            speed = 0.1f * (maxDistance / distance);
            relativePosition = transform.position - player.transform.position;
            controller.Move(relativePosition * Time.deltaTime * speed);
            
        }
    }
}
