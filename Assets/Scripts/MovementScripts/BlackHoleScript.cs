using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlackHoleScript : MonoBehaviour
{
    public Player_Space_Ship_Movement player;
    public Player_Asset_Manager assetManager;
    public float maxDistance;
    private float distance;
    private Vector3 relativePosition;
    private CharacterController controller;
    private float speed;
    private float damage;

    private void Start()
    {
        controller = player.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < maxDistance)
        {
            if (distance < 500)
            {
                damage = 10f * ((maxDistance - distance) / maxDistance) * Time.deltaTime;
                assetManager.VariableDamage(damage);
            }


            speed = 0.5f * ((maxDistance  - distance) / maxDistance);
            relativePosition = transform.position - player.transform.position;
            controller.Move(relativePosition * Time.deltaTime * speed);
            
        }
    }
}
