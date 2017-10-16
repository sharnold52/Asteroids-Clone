using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAsteroidSpawner : MonoBehaviour
{
    //array of prefabs to choose from
    public GameObject[] appearance;


    // track number of asteroids and current asteroid
    public int asteroidMax = 6;
    public int asteroidCount = 0;
    public float speedMax = 0.1f;
    GameObject current;

    // values that decide spawn location and asteroid to spawn
    int spawnLoc;
    int randomPrefab;
    Vector3 position;

    // velocity and direction
    float velocity;
    Vector3 direction;

    // values for screen size
    float leftSide = 0.0f;
    float rightSide = 0.0f;
    float bottom = 0.0f;
    float top = 0.0f;
    float distanceZ = 10.0f;

    float buffer = 0.5f;

    // Use this for initialization
    void Start ()
    { 
        position = Vector3.zero;

        // Gets screen size
        leftSide = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).x;
        rightSide = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, distanceZ)).x;
        top = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, distanceZ)).y;
        bottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).y;
    }

    // Update
    private void FixedUpdate()
    {
        // check to see if there are too many asteroids
        if (asteroidCount > asteroidMax)
        {

        }
        else
        {
            // decide spawning location for asteroid and which asteroid to spawn
            spawnLoc = Random.Range(0, 4);
            randomPrefab = Random.Range(0, appearance.Length);

            //spawns at top
            if (spawnLoc == 0)
            {
                // randomly spawn it
                position.y = top + buffer;
                position.x = Random.Range(leftSide, rightSide);
                current = Instantiate(appearance[randomPrefab], position, Quaternion.Euler(0, 0, Random.Range(0, 361)));

                // random direction and velocity
                velocity = Random.Range(0.001f, speedMax);
                direction.x = Random.Range(-1f, 1f);
                direction.y = Random.Range(-1f, -.1f);
                direction = direction.normalized * velocity;

                // set asteroids velocity
                current.GetComponent<MoveAsteroid>().velocity = direction;
            }

            // spawn at bottom
            else if (spawnLoc == 1)
            {
                // randomly spawn it
                position.y = bottom - buffer;
                position.x = Random.Range(leftSide, rightSide);
                current = Instantiate(appearance[randomPrefab], position, Quaternion.Euler(0, 0, Random.Range(0, 361)));

                // random direction and velocity
                velocity = Random.Range(0.001f, speedMax);
                direction.x = Random.Range(-1f, 1f);
                direction.y = Random.Range(.1f, 1f);
                direction = direction.normalized * velocity;

                // set asteroids velocity
                current.GetComponent<MoveAsteroid>().velocity = direction;
            }

            // spawn at left
            else if (spawnLoc == 2)
            {
                // randomly spawn it
                position.y = Random.Range(bottom, top);
                position.x = leftSide - buffer;
                current = Instantiate(appearance[randomPrefab], position, Quaternion.Euler(0, 0, Random.Range(0, 361)));

                // random direction and velocity
                velocity = Random.Range(0.001f, speedMax);
                direction.x = Random.Range(0.1f, 1f);
                direction.y = Random.Range(-1f, 1f);
                direction = direction.normalized * velocity;

                // set asteroids velocity
                current.GetComponent<MoveAsteroid>().velocity = direction;
            }

            // spawn at right
            else if ( spawnLoc == 3)
            {
                // randomly spawn it
                position.y = Random.Range(bottom, top);
                position.x = rightSide + buffer;
                current = Instantiate(appearance[randomPrefab], position, Quaternion.Euler(0, 0, Random.Range(0, 361)));

                // random direction and velocity
                velocity = Random.Range(0.001f, speedMax);
                direction.x = Random.Range(-1f, -.1f);
                direction.y = Random.Range(-1f, 1f);
                direction = direction.normalized * velocity;

                // set asteroids velocity
                current.GetComponent<MoveAsteroid>().velocity = direction;
            }

            // increment asteroid count
            asteroidCount += 1;
            Debug.Log(asteroidCount);
        }
    }
}
