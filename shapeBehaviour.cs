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
	private bool setTime = false;

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
		
			if(triggerOff && hasChild()!=null)
			{
				shape = hasChild();
				isTouching = Physics2D.OverlapCircle(shape.position , radius , onShape); //is the shape touching another shape?

				
			}			
			
			//if the shape isnt touching another shape and its falling turn boxcoll on
			if(rb.velocity.y<0 && coll.isTrigger && !isTouching && triggerOff)
			{
				coll.isTrigger=false;
				triggerOff=false;
				Destroy(transform.GetChild(0).gameObject);
				gameObject.layer=8;
				gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Shape";
				//rb.constraints = RigidbodyConstraints2D.None; // turn off freeze rotation

			}

		if (!triggerOff && rb.velocity.y < 0.1f && rb.velocity.x < 0.1f &&
		    rb.transform.InverseTransformDirection(rb.velocity).z < 0.1f && rb.velocity.y > -0.1f &&
		    rb.velocity.x > -0.1f &&
		    rb.transform.InverseTransformDirection(rb.velocity).z > -0.1f)
		{
			if (!setTime)
			{
				nextTime = Time.time + 2f;
				setTime = true;
			}

			if (Time.time > nextTime)
			{
				Debug.Log("freezed");
				//rb.constraints = RigidbodyConstraints2D.FreezePositionX;
				rb.mass = 5f;
				setTime = false;
			}
		}
		else
			setTime = false;

		//TODO
			if(!triggerOff && transform.position.y<-3)
			{
				HeightFinder.lives--;
				HeartsUi.heartsUi.heartsDown((int)HeightFinder.lives+3);
				Destroy(gameObject);
			}
	}
	
	Transform hasChild(){
		if(transform.childCount > 0)
		{
			    foreach(Transform child in transform) 
				{
					return child;
				}
		}
		return null;
	}
	
	

}
