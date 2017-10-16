using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoAsteroidCollider : MonoBehaviour
{
    // bounding sphere
    public float radius;

    // Use this for initialization
    void Start ()
    {
        radius = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
