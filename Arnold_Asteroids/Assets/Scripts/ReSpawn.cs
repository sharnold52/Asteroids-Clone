using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawn : MonoBehaviour
{
    // number of lives the player has left
    public int lives = 3;

    // is the game over?
    bool gameOver = false;
    public Texture2D gameOverText;

    // array of lifepoints
    public Texture2D lifePoint;

    // the player object
    public GameObject preFab;
    GameObject player;

    // menu button
    public GameObject menuButton;

    // int to track how many times the player flashed flashing
    int flashing = 0;

    private GUIStyle guiStyle = new GUIStyle();


    // print lives left on screen
    private void OnGUI()
    {
        // font-size
        guiStyle.fontSize = 60;
        guiStyle.normal.textColor = Color.white;

        // label
        GUI.Label(new Rect(Screen.width - 450, 0, 600, 600), "Lives = ", guiStyle);

        // Print out lives that are left
        for (int i = 0; i < lives; i++)
        {
            GUI.DrawTexture(new Rect(Screen.width - 250 + i * 70, 0, 60, 60), lifePoint);
        }

        // game over
        if (gameOver)
        {
            GUI.DrawTexture(new Rect(Screen.width/2 - 200, Screen.height/2 - 100, 400, 200),gameOverText);
        }
    }

    // respawn player
    public void Spawn()
    {
        if (lives > 0)
        {
            // reset player position to start
            player = Instantiate(preFab, Vector3.zero, Quaternion.identity);

            // disable player collider for a little bit
            player.GetComponent<PlayerCollider>().enabled = false;

            // increment lives count down
            lives--;

            // make player flash for a little bit then stop
            InvokeRepeating("PlayerFlashing", 0, 0.3f);
 

            // wait a little before activating player collider
            Invoke("TurnOnCollider", 3);
        }
        else
        {
            Destroy(FindObjectOfType<PlayerMovement>().gameObject);
            gameOver = true;
            Instantiate(menuButton);
        }
    }

    void TurnOnCollider()
    {
        if(player != null)
        {

            player.GetComponent<PlayerCollider>().enabled = true;
        }
    }

    // make player flash when they first spawn
    void PlayerFlashing()
    {
        if(player != null)
        {
            if (player.GetComponent<SpriteRenderer>().enabled == true)
            {
                player.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().enabled = true;
            }

            // increment flashing
            flashing++;
        }

        // cancel flashing after set time
        if (flashing >= 10)
        {
            CancelInvoke("PlayerFlashing");
            player.GetComponent<SpriteRenderer>().enabled = true;
            flashing = 0;
        }
    }
}
