using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour {
	private Vector3 shootPosition,aimPosition;
	[SerializeField]
	private int x,y,z,shootingPower;
	double lastz,zf,current;
	bool rot=false,rot2=false,loading;
	float nextTime=0;
	public float shootingSpeed=1f;
	int i=0;
	PredictionLine predictionLine;

	// Use this for initialization
	void Start () {
		shootPosition= new Vector3(x,y,z);
		predictionLine=GetComponent<PredictionLine>();
		}
	
	// Update is called once per frame
	void Update () {
		if(loading && Time.time<nextTime){
			return;
			
		}
		else
		{
			nextTime=Time.time+shootingSpeed;
			loading=false;
		}
		rot2=false;
		
		if(pauseMenu.GameIsPaused)
			return;
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
	
			 switch(touch.phase)	
			{
				case TouchPhase.Began:
					aimPosition = new Vector3(touch.position.x,touch.position.y,0);
					rot2 = true;
					rot=true;
				break;
				
				case TouchPhase.Moved:
					aimPosition = new Vector3(touch.position.x,touch.position.y,0);
					rot=true;
					rot2=true;
				break;
				
				case TouchPhase.Ended:
				if(transform.GetChild(0) != null)
				{
					shoot(touch);
				}
				break;
			} 
		}
	 		if(rot){
			aim();
		} 
	
	}
	
	void shoot(Touch touch) // shoots shape and loads the next one
	{
		transform.GetChild(0).GetComponent<Rigidbody2D>().isKinematic = false; // Rigidbody2D effect on
		Vector3 diff = Camera.main.ScreenToWorldPoint(aimPosition) - transform.position;
		diff.Normalize();
		transform.GetChild(0).GetComponent<Rigidbody2D>().velocity=(diff)*16;
		transform.DetachChildren();
		GetComponent<shapeGenerator>().onCannonLoaded();
		loading=true;
		predictionLine.changeOpacity();
		// predictionLine.clearDots();
	}
	
	void aim() // aims on touch
	{
		Vector3 diff = Camera.main.ScreenToWorldPoint(aimPosition) - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		if(rot2)
		predictionLine.paintDotedLine(diff*16,transform.GetChild(0).position);
		
	}
	
}
