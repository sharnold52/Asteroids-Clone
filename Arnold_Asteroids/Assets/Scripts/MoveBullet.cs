using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    // player object
    PlayerMovement player;

    // current velocity of player
    private Vector3 pVel;
    
    // initial starting velocity of bullet and final velocity (taking player's velocity into account)
    private float velInitial = .005f;
    public Vector3 velFinal;

    // position and direction of bullet 
    private Vector3 pos;
    private Vector3 direction;


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
        // size of screen
        leftSide = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).x;
        rightSide = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, distanceZ)).x;
        top = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, distanceZ)).y;
        bottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).y;

        // gets position
        pos = transform.position;

        // gets player movement script
        player = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));

        // direction vector values
        float xDir = Mathf.Cos((transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad) * Mathf.Rad2Deg;
        float yDir = Mathf.Sin((transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad) * Mathf.Rad2Deg;

        // get's velocity and direction of player when bullet is first fired
        pVel = player.velocity;
        direction = new Vector3(xDir, yDir, 0);

        // calculates final velocity
        velFinal = (direction * velInitial) + pVel;
	}

    // Update
    private void FixedUpdate()
    {
        pos += velFinal;
        transform.position = pos;

        // checks for out of bounds and destroys bullet
        if (pos.x < leftSide - buffer)
        {
            Destroy(gameObject);
        }

        if (pos.x > rightSide + buffer)
        {
            Destroy(gameObject);
        }

        if (pos.y < bottom - buffer)
        {
            Destroy(gameObject);
        }

        if (pos.y > top + buffer)
        {
            Destroy(gameObject);
        }
    }
}
