using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public Transform asteroid1, asteroid2, asteroid3;
    public int asteroidAmount, sphereRadius;
    public Transform temp;
    public float rotationSpeed;

    RandomRotator rotatorRef;
    
    
  
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < asteroidAmount; ++i)
        {
            InstantiateAsteroid(asteroid1);
            InstantiateAsteroid(asteroid2);
            InstantiateAsteroid(asteroid3);
        }
    }

    void InstantiateAsteroid(Transform asteroid)
    {   
        //add transform.position because Random.onUnitSphere will default to origin
        temp = Instantiate(asteroid, (Random.onUnitSphere * sphereRadius)+transform.position, Random.rotation);

        //set random range of sizes of asteroids
        temp.localScale = temp.localScale * Random.Range(0.5f, 15f);
        rotatorRef = temp.GetComponent<RandomRotator>();
        rotatorRef.SetSphere(GetComponent<Transform>());
        rotatorRef.SetRotationSpeed(rotationSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
