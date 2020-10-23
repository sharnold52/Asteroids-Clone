using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    // score tracker
    public float score = 0;


    private GUIStyle guiStyle = new GUIStyle();


    private void Start()
    {
        
    }

    // increment score for first level asteroids
    public void FirstLevelScore()
    {
        score += 50f;
    }

    // increment score for second level asteroids
    public void SecondLevelScore()
    {
        score += 100f;
    }

    // increment score for getting hit by first level asteroid... freebie
    public void HalfScore()
    {
        score += 25f;
    }

    private void OnGUI()
    {
        // font-size
        guiStyle.fontSize = 60;
        guiStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(10.0f, 0.0f, 1000.0f, 1000.0f), "Score = " + score.ToString(), guiStyle);
    }
}

