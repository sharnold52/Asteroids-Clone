using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneAsteroidCollider : MonoBehaviour
{
    // bounding sphere
    public float radius;

    // current position and velocity of this asteroid
    Vector3 pos;
    Vector3 velocity;
    

    // spawning second level asteroids
    // array of prefabs to choose from and value to choose asteroid appearance
    public GameObject[] appearance;
    int randomPrefab;

    // max speed value, current asteroid, and number of asteroids to spawn
    public float speedMax = 0.1f;
    GameObject current;
    int amount;

    // speed and direction
    Vector3 direction;
    float speed;


    // Use this for initialization
    void Start ()
    {
        radius = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
        velocity = gameObject.GetComponent<MoveAsteroid>().velocity;
	}

    // Update
    private void FixedUpdate()
    {
        // update position
        pos = transform.position;
    }

    // spawns second level asteroids when this object is destroyed
    public void SpawnSecondLevel()
    {
        // determine how many second level asteroids to spawn
        amount = Random.Range(2, 3);

        // spawn asteroids
        for (int i = 0; i < amount; i++)
        {
            // randomly choose prefab
            randomPrefab = Random.Range(0, appearance.Length);

            // create asteroid
            current = Instantiate(appearance[randomPrefab], pos, Quaternion.Euler(0, 0, Random.Range(0, 361)));

            // determine direction variation and speed
            direction.x = Random.Range(0, 1f);
            direction.y = 1 - direction.x;
            speed = Random.Range(0.01f, velocity.magnitude);

            // normalize direction and set velocity
            direction.x = direction.x * velocity.x;
            direction.y = direction.y * velocity.y;
            current.GetComponent<MoveAsteroid>().velocity = direction.normalized * speed;
        }

    }
}
