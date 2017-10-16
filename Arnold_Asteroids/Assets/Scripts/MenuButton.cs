using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    // button
    public GameObject button;

    // mouse position
    Vector3 mousePos;

    // bool for hovering
    bool hover;

    // update
    private void FixedUpdate()
    {
        // mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        // check for mouse over
        if (mousePos.x > gameObject.GetComponent<SpriteRenderer>().bounds.min.x)
        {
            hover = true;
        }
        else
        {
            hover = false;
        }

        if (mousePos.x < gameObject.GetComponent<SpriteRenderer>().bounds.max.x)
        {

        }
        else
        {
            hover = false;
        }

        if (mousePos.y > gameObject.GetComponent<SpriteRenderer>().bounds.min.y)
        {
            hover = true;
        }
        else
        {
            hover = false;
        }

        if (mousePos.y < gameObject.GetComponent<SpriteRenderer>().bounds.max.y)
        {

        }
        else
        {
            hover = false;
        }

        if (hover)
        {
            button.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            button.GetComponent<SpriteRenderer>().color = Color.white;
        }

        // check for mouse down
        if (hover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("menu");
        }
    }

}
