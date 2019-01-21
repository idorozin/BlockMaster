using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shapeBehaviour : MonoBehaviour {
	Rigidbody2D rb;
	public Collider2D coll;
	bool triggerOff=true;
	public bool isTouching=true;
	
	private Transform shape;
	public float radius;
	public LayerMask onShape;
	private float nextTime=0;
	private bool setTime = false , done=false;
	// Use this for initialization
	void Start () 
	{
		coll=GetComponent<Collider2D>();
		coll.isTrigger=true;
		GetComponent<Rigidbody2D>().isKinematic=true; // turn off the effect of the rigidbody2D
		rb = GetComponent<Rigidbody2D>();
		//transform.eulerAngles = new Vector3(transform.parent.eulerAngles.x,transform.parent.eulerAngles.y,transform.parent.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () 
	{
			if(triggerOff)
			{

				isTouching = isTouchingShapes(); //is the shape touching another shape?
			}			
			
			//if the shape isn't touching another shape and its falling turn boxcoll on
			turnOnCollider();
			//wait period of time and freezes the rigidbody
			freezeRigidbody();
			//check if the shape fell down if it did , Destroy game object and lives--
			healthDown();		
	}
	
	//return the first child
	bool isTouchingShapes(){
		if(transform.childCount > 0)
		{
			    foreach(Transform child in transform)
			    {
				    if (Physics2D.OverlapCircle(child.position, radius, onShape)) return true;
			    }
		}
		return false;
	}

	private void turnOnCollider()
	{
		if(rb.velocity.y<0 && coll.isTrigger && !isTouching && triggerOff)
		{
			coll.isTrigger=false;
			triggerOff=false;
			Destroy(transform.GetChild(0).gameObject);
			gameObject.layer=8;
			//rb.constraints = RigidbodyConstraints2D.None; // turn off freeze rotation
			gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Shape";
			gameObject.GetComponent<TrailRenderer>().sortingLayerName = "Trail_";
		}
	}

	private void freezeRigidbody()
	{
		if (!triggerOff && rb.velocity.y < 0.1f && rb.velocity.x < 0.1f &&
		    rb.transform.InverseTransformDirection(rb.velocity).z < 0.1f && rb.velocity.y > -0.1f &&
		    rb.velocity.x > -0.1f &&
		    rb.transform.InverseTransformDirection(rb.velocity).z > -0.1f
		    &&!done)
		{
			if (!setTime)
			{
				nextTime = Time.time + 2f;
				setTime = true;
			}

			if (Time.time > nextTime)
			{
				rb.constraints = RigidbodyConstraints2D.FreezeAll;
				//rb.mass = 5f;
				setTime = false;
				Destroy(gameObject.GetComponent<TrailRenderer>());
				done = true;
			}
		}
		else
			setTime = false;

	}

	private void healthDown()
	{
		//TODO
		if(!triggerOff && transform.position.y<-3)
		{
			HeightFinder.lives--;
			HeartsUi.heartsUi.heartsDown((int)HeightFinder.lives+3);
			Destroy(gameObject);
		}
	}




}
