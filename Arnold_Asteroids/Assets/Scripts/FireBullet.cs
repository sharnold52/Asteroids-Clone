using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    // bullet prefab and player game object
    public GameObject bullet;
    public GameObject player;

    // bullet direction, position, and velocity
    private Vector3 direction;
    private Vector3 position;

    // staggers bullets
    int fireRate = 0;


	// Use this for initialization
	void Start ()
    {
        // gets players direction, position, and velocity
        direction = player.GetComponent<PlayerMovement>().direction;
        position = player.GetComponent<PlayerMovement>().vehiclePosition;
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        // checks for button press then fires bullet if true
        if (Input.GetKey(KeyCode.Space) && fireRate == 0)
        {
            Fire();

            // increments for first time
            fireRate++;
        }

        // increments fire rate
        if (fireRate != 0)
        {
            fireRate++;
        }

        // resets fire rate
        if (fireRate >= 30)
        {
            fireRate = 0;
        }
    }

    // fires the bullet
    public void Fire()
    {

        // updates players direction, position, and velocity
        direction = player.GetComponent<PlayerMovement>().direction;
        position = player.GetComponent<PlayerMovement>().vehiclePosition;

        // calculates bullet spawn position
        position = position + ( 0.5f * direction);

        // spawns bullet
        GameObject current = Instantiate(bullet, transform.position + 4 * transform.forward, transform.rotation);
        current.transform.position = position;
    }
}
