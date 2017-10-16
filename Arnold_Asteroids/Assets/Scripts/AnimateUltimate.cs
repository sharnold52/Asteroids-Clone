using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateUltimate : MonoBehaviour
{
    //scale of ultimate
    Vector3 size;
    int tracker = 0;
    int maxSize = 100;

    // array to get all asteroids and get the asteroid manager
    MoveAsteroid[] allAsteroids;
    FirstAsteroidSpawner manager;

    // distances to check for collisions with ultimate ability
    float acceptableDistance;
    float distance;

	// Use this for initialization
	void Start ()
    {
        // get size
        size = gameObject.transform.localScale;

        // get asteroid manager
        manager = FindObjectOfType<FirstAsteroidSpawner>();
	}

    // Update
    private void FixedUpdate()
    {
        // update all asteroids
        allAsteroids = FindObjectsOfType<MoveAsteroid>();

        // check for collision with all asteroids
        for(int i = 0; i < allAsteroids.Length; i++)
        {
            distance = Mathf.Pow(allAsteroids[i].gameObject.transform.position.x - gameObject.transform.position.x, 2) + Mathf.Pow(allAsteroids[i].gameObject.transform.position.y - gameObject.transform.position.y, 2);
            acceptableDistance = allAsteroids[i].gameObject.GetComponent<SpriteRenderer>().bounds.extents.x + gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
            acceptableDistance = Mathf.Pow(acceptableDistance, 2);

            // destroy colliding objects
            if (distance < acceptableDistance)
            {
                Destroy(allAsteroids[i].gameObject);
                if (manager.asteroidCount != 0)
                {
                    manager.asteroidCount--;
                }
            }
        }

        // update size
        size = gameObject.transform.localScale;

        // increment speed of size change
        if (tracker >= 0 && tracker < 25)
        {
            // change size
            size.x += 0.1f;
            size.y += 0.1f;
        }
        else if (tracker >= 25 && tracker < 50)
        {
            // change size
            size.x += 0.2f;
            size.y += 0.2f;
        }
        else if (tracker >= 50 && tracker < 75)
        {
            // change size
            size.x += 0.3f;
            size.y += 0.3f;
        }
        else
        {
            // change size
            size.x += 0.4f;
            size.y += 0.4f;
        }

        // set ultimate's size and increment tracker
        gameObject.transform.localScale = size;
        tracker++;

        // check to see if ultimate object should be deleted
        if (tracker > maxSize)
        {
            Destroy(gameObject);
        }
    }
}
