using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // radius of player bounding circle
    public GameObject player;
    public float playerRadius;

    // radius of first level asteroids
    public float astRadius;

    // distance between player and asteroid
    public float distance;
    public float acceptableDistance;

    // asteroid manager to keep track of asteroid count and score to keep track of score
    FirstAsteroidSpawner asteroidManager;
    Score currentScore;

    // array of all the first level asteroids
    OneAsteroidCollider[] asteroids;

    // array of all second level asteroids
    TwoAsteroidCollider[] smallAsteroids;

    // Respawning player
    ReSpawn spawner;

	// Use this for initialization
	void Start ()
    {
        // get asteroid manager,spawner, and score
        asteroidManager = FindObjectOfType<FirstAsteroidSpawner>();
        currentScore = FindObjectOfType<Score>();
        spawner = FindObjectOfType<ReSpawn>();

        // get player bounds
        Bounds pBounds = player.GetComponent<SpriteRenderer>().bounds;
        playerRadius = pBounds.extents.x;

        // get all first level asteroids
        asteroids = (OneAsteroidCollider[])FindObjectsOfType(typeof(OneAsteroidCollider));

        // get all second level asteroids
        smallAsteroids = (TwoAsteroidCollider[])FindObjectsOfType(typeof(TwoAsteroidCollider));
    }

    // Update
    private void FixedUpdate()
    {
        // update all first level asteroids
        asteroids = (OneAsteroidCollider[])FindObjectsOfType(typeof(OneAsteroidCollider));

        // update all second level asteroids
        smallAsteroids = (TwoAsteroidCollider[])FindObjectsOfType(typeof(TwoAsteroidCollider));

        // go through  first level asteroids
        for (int i = 0; i < asteroids.Length; i++)
        {
            // calculate and update distance
            distance = Mathf.Pow(player.transform.position.x - asteroids[i].transform.position.x, 2) + Mathf.Pow(player.transform.position.y - asteroids[i].transform.position.y, 2);
            acceptableDistance = Mathf.Pow(asteroids[i].radius + playerRadius, 2);

            // check collision with player
            if (distance < acceptableDistance)
            {
                Destroy(gameObject);
                asteroids[i].GetComponent<OneAsteroidCollider>().SpawnSecondLevel();
                Destroy(asteroids[i].gameObject);

                // increment down asteroid count
                asteroidManager.asteroidCount--;

                // increment score up.. little bit of a freebie, but only half the regular score! Also, potentially respawn player
                currentScore.HalfScore();
                spawner.Spawn();
            }
        }

        // go through  second level asteroids
        for (int i = 0; i < smallAsteroids.Length; i++)
        {
            // calculate and update distance
            distance = Mathf.Pow(player.transform.position.x - smallAsteroids[i].transform.position.x, 2) + Mathf.Pow(player.transform.position.y - smallAsteroids[i].transform.position.y, 2);
            acceptableDistance = Mathf.Pow(smallAsteroids[i].radius + playerRadius, 2);

            // check collision with player
            if (distance < acceptableDistance)
            {
                Destroy(gameObject);
                Destroy(smallAsteroids[i].gameObject);

                // increment score up.. little bit of a freebie, but only a first level Score! Also, potentially respawn player
                currentScore.FirstLevelScore();
                spawner.Spawn();
            }
        }

    }

}
