using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // player's prefab and position
    public GameObject playerPrefab;
    public Vector3 vehiclePosition;
    private float zRotation;


    // vectors for player direction and velocity
    public Vector3 direction;
    public Vector3 velocity;

    // acceleration
    public Vector3 acceleration;
    float accelerationRate;
    float deceleration;

    // speed limit
    float maxSpeed;


    // screen wrapping stuff
    
    // tracks right and left camera sides
    float leftSide = 0.0f;
    float rightSide = 0.0f;
    float bottom = 0.0f;
    float top = 0.0f;

    //value that makes it so ship disappears offscreen before reappearing on other side
    float buffer = 0.5f;
    float distanceZ = 10.0f;

    // Use this for initialization
    void Start ()
    {
        //grabs position at start
		vehiclePosition = transform.position;
        zRotation = transform.rotation.z;

        // sets velocity and accelleration
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        accelerationRate = 0.001f;
        deceleration = 0.94f;
        maxSpeed = 0.3f;

        //starts vehicle facing the right
        direction = new Vector3(1f,0,0);



        // uses a specific distance for screen wrapping
        leftSide = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).x;
        rightSide = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, distanceZ)).x;
        top = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, distanceZ)).y;
        bottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).y;

    }

    // update player movement
    private void FixedUpdate()
    {
        float deltaAngle = 4f;

        // move player forward
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Calculate acceleration
            acceleration = direction * accelerationRate;
        }
        else
        {
            // decelerate the car
            acceleration = acceleration * deceleration;
            velocity = velocity * deceleration;
        }

        // turn the player
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //positive rotation
            direction = Quaternion.Euler(0, 0, deltaAngle) * direction;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // negative rotation
            direction = Quaternion.Euler(0, 0, -deltaAngle) * direction;
        }

        // calculate the rotation of player image
        zRotation = (Mathf.Atan2(direction.x, direction.y) * -(180 / Mathf.PI));

        // Add acceleration to velocity 
        velocity += acceleration;

        // Limit the velocity so it doesn’t move too quickly 
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Draw the vehicle at that position 
        vehiclePosition += velocity;
        transform.position = vehiclePosition;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
        zRotation = transform.rotation.z;


        //screenwrapping stuff
        if (vehiclePosition.x < leftSide - buffer)
        {
            vehiclePosition.x = rightSide + buffer;
            transform.position = vehiclePosition;
        }

        if (vehiclePosition.x > rightSide + buffer)
        {
            vehiclePosition.x = leftSide - buffer;
            transform.position = vehiclePosition;
        }

        if (vehiclePosition.y < bottom - buffer)
        {
            vehiclePosition.y = top + buffer;
            transform.position = vehiclePosition;
        }

        if (vehiclePosition.y > top + buffer)
        {
            vehiclePosition.y =  bottom - buffer;
            transform.position = vehiclePosition;
        }

        // set new position
        transform.position = vehiclePosition;
        vehiclePosition = transform.position;
    }
}
