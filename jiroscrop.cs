using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jiroscrop : MonoBehaviour {
    
    public Transform surface;

	private Rigidbody2D rb;
	// Use this for initialization
	void Start ()
	{
		rb = surface.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = transform.eulerAngles = new Vector3(0,0,rb.angularVelocity*300);
		/*if(rb.angularVelocity.magnitude >0)
			transform.eulerAngles = transform.eulerAngles = new Vector3(0,0,45f);
		if(rb.angularVelocity.magnitude >0)
			transform.eulerAngles = transform.eulerAngles = new Vector3(0,0,-45f);*/
	}
}
