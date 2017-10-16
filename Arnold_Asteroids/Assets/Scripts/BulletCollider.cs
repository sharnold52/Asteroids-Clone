using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    // bullet point
    Vector3 point;

    // radius of first level asteroids
    public float astRadius;

    // distance between bullet and asteroid
    public float distance;
    public float acceptableDistance;

    // array of all the first level asteroids
    OneAsteroidCollider[] asteroids;

    // array of all the second level asteroids
    TwoAsteroidCollider[] smallAsteroids;

    // get other scripts to keep track of asteroids, score, and ultimate ability
    FirstAsteroidSpawner asteroidManager;
    Score currentScore;
    UltimateAbility ability;

    // Use this for initialization
    void Start ()
    {
        // get asteroid manager, score, and ultimate abilitiy
        asteroidManager = FindObjectOfType<FirstAsteroidSpawner>();
        currentScore = FindObjectOfType<Score>();
        ability = FindObjectOfType<UltimateAbility>();

        // get point of bullet
        point = transform.position;

        // get all first level asteroids
        asteroids = (OneAsteroidCollider[])FindObjectsOfType(typeof(OneAsteroidCollider));

        // get all second level asteroids
        smallAsteroids = (TwoAsteroidCollider[])FindObjectsOfType(typeof(TwoAsteroidCollider));
    }
	
	// Update is called once per frame
	void Update ()
    {
        // update point of bullet
        point = transform.position;

        // update all first level asteroids
        asteroids = (OneAsteroidCollider[])FindObjectsOfType(typeof(OneAsteroidCollider));

        // update all second level asteroids
        smallAsteroids = (TwoAsteroidCollider[])FindObjectsOfType(typeof(TwoAsteroidCollider));

        // go through first-level asteroids
        for (int i = 0; i < asteroids.Length; i++)
        {
            // calculate distance
            distance = Mathf.Pow(point.x - asteroids[i].transform.position.x, 2) + Mathf.Pow(point.y - asteroids[i].transform.position.y, 2);
            acceptableDistance = Mathf.Pow(asteroids[i].radius, 2);

            // check for collision
            if (distance < acceptableDistance)
            {
                Destroy(gameObject);
                asteroids[i].GetComponent<OneAsteroidCollider>().SpawnSecondLevel();
                Destroy(asteroids[i].gameObject);

                // increment asteroid count down and increment score and ability charge up 
                asteroidManager.asteroidCount--;
                currentScore.FirstLevelScore();
                ability.FirstLevelCharge();
            }
        }

        // go through second-level asteroids
        for (int i = 0; i < smallAsteroids.Length; i++)
        {
            // calculate distance
            distance = Mathf.Pow(point.x - smallAsteroids[i].transform.position.x, 2) + Mathf.Pow(point.y - smallAsteroids[i].transform.position.y, 2);
            acceptableDistance = Mathf.Pow(smallAsteroids[i].radius, 2);

            // check for collision
            if (distance < acceptableDistance)
            {
                Destroy(gameObject);
                Destroy(smallAsteroids[i].gameObject);

                // increment score and ability charge up
                currentScore.SecondLevelScore();
                ability.SecondLevelCharge();
            }
        }
    }
}
