using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAbility : MonoBehaviour
{
    // textures for charge bar
    public Texture2D barTexture;
    public Texture2D barTextureReady;

    // prefab for ultimate ability
    public GameObject ultimatePrefab;

    // the player to track their position
    GameObject player;

    // get and score
    Score scoreBoard;

    // charge bar for ultimate
    public float charge;
    public float chargeBar = 100;

    // is ultimate ready?
    public bool ready;


    private GUIStyle guiStyle = new GUIStyle();

    // Use this for initialization
    void Start ()
    {
        charge = 0;
        ready = false;

        // get player, spawner, and score
        player = FindObjectOfType<PlayerMovement>().gameObject;
        scoreBoard = FindObjectOfType<Score>();
	}

    // Update 
    private void FixedUpdate()
    { 
        // check to see if charge is ready
        if (charge >= chargeBar)
        {
            ready = true;
            charge = chargeBar;
        }
        else
        {
            ready = false;
        }

        // change color of charge bar to indicate ultimate is ready
        if (ready)
        {
            // check for ultimate activation!
            if(Input.GetKey(KeyCode.DownArrow))
            {
                // update player
                player = FindObjectOfType<PlayerMovement>().gameObject;

                // start ultimate ability
                Instantiate(ultimatePrefab, player.transform.position, Quaternion.identity);

                // reset charge
                charge = 0;

                // increment score with lump sum
                scoreBoard.score += 500;
                
            }
        }
    }

    // Interface to tell player if ultimate is ready
    private void OnGUI()
    {
        // font-size
        guiStyle.fontSize = 22;
        guiStyle.normal.textColor = Color.white;


        if (ready)
        {
            GUI.DrawTexture(new Rect((Screen.width / 2) - 200, 5, charge * 4, Screen.height / 18), barTextureReady);
            GUI.Label(new Rect((Screen.width / 2) - 145, 8, 200, 200), "Press Down Arrow for Ultimate", guiStyle);
        }
        else
        {
            GUI.DrawTexture(new Rect((Screen.width / 2) - 200, 0, charge * 4, Screen.height / 18), barTexture);
        }
    }

    // increment score for destroying first level asteroid
    public void FirstLevelCharge()
    {
        if (charge + 5 < 100)
        {

            charge += 5;
        }
        else
        {
            charge = 100;
        }
    }

    // increment score for destroying second level asteroid
    public void SecondLevelCharge()
    {
        if (charge + 10 < 100)
        {

            charge += 10;
        }
        else
        {
            charge = 100;
        }
    }
}
