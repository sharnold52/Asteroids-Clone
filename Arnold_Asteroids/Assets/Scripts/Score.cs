using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    // score tracker
    public float score = 0;
    

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
        GUI.Label(new Rect(10, 0, 100, 100), "Score = " + score.ToString());
    }
}

