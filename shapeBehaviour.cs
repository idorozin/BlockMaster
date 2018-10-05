using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shapeBehaviour : MonoBehaviour {
	Rigidbody2D rb;
	public BoxCollider2D coll;
	bool triggerOff=true;
	public bool isTouching=true;
	
	private Transform shape;
	public float radius;
	public LayerMask onShape;

	// Use this for initialization
	void Start () 
	{
		coll=GetComponent<BoxCollider2D>();
		coll.isTrigger=true;
		GetComponent<Rigidbody2D>().isKinematic=true; // turn off the effect of the rigidbody2D
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
			if(triggerOff && hasChild()!=null)
			{
				shape = hasChild();
				isTouching = Physics2D.OverlapCircle(shape.position , radius , onShape); //is the shape touching another shape?
			}
			/* if(rb.velocity.y>=0 && !coll.isTrigger && !triggerOff)
			{
				coll.isTrigger=true;
				triggerOff=true;
			} */
			
			//if the shape isnt touching another shape and its falling turn boxcoll on
			if(rb.velocity.y<0 && coll.isTrigger && !isTouching && triggerOff)
			{
				gameObject.layer=8;
				coll.isTrigger=false;
				triggerOff=false;
				Destroy(transform.GetChild(0).gameObject);
				rb.constraints = RigidbodyConstraints2D.None; // turn off freeze rotation

			}
			if(!triggerOff && transform.position.y<-3)
			{
				HeightFinder.lives--;
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
