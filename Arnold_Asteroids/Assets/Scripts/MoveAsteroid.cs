using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAsteroid : MonoBehaviour
{
    // how fast is the asteroid moving?
    public Vector3 velocity;

    // is this a second level asteroid?
    public bool secondLevel;

    // get script of asteroid spawner to tell it when this object is destroyed
    FirstAsteroidSpawner spawner;

    // values to check if bullet is on screen

    // tracks right and left camera sides
    float leftSide = 0.0f;
    float rightSide = 0.0f;
    float bottom = 0.0f;
    float top = 0.0f;

    //value that makes it so ship disappears offscreen before reappearing on other side
    float buffer = 1.0f;
    float distanceZ = 10.0f;

    // Use this for initialization
    void Start ()
    {
        // get script data
        spawner = (FirstAsteroidSpawner)FindObjectOfType(typeof(FirstAsteroidSpawner));

        // size of screen
        leftSide = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).x;
        rightSide = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, distanceZ)).x;
        top = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, distanceZ)).y;
        bottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // updates position
        transform.position += velocity;

        // checks for out of bounds and destroys bullet
        if (transform.position.x < leftSide - buffer)
        {
            Destroy(gameObject);
            // increment count down if it isn't a second level asteroid
            if (!secondLevel)
            {
                spawner.asteroidCount -= 1;
            }
        }

        if (transform.position.x > rightSide + buffer)
        {
            Destroy(gameObject);
            // increment count down if it isn't a second level asteroid
            if (!secondLevel)
            {
                spawner.asteroidCount -= 1;
            }
        }

        if (transform.position.y < bottom - buffer)
        {
            Destroy(gameObject);
            // increment count down if it isn't a second level asteroid
            if (!secondLevel)
            {
                spawner.asteroidCount -= 1;
            }
        }

        if (transform.position.y > top + buffer)
        {
            Destroy(gameObject);
            // increment count down if it isn't a second level asteroid
            if (!secondLevel)
            {
                spawner.asteroidCount -= 1;
            }   
        }
    }
}
