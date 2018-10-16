using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour {
	private Vector3 initialPosition,aimPosition;
	[SerializeField]
	private int x,y,z,shootingPower;
	double lastz,zf,current;
	bool fingerMoved=false,loading,cantShoot;
	float nextTime=0;
	public float shootingSpeed=1f;
	PredictionLine predictionLine;

	// Use this for initialization
	void Start () {
		predictionLine=GetComponent<PredictionLine>();
		}
	
	// Update is called once per frame
	void Update () {
		if(loading && Time.time<nextTime)
		{
			cantShoot = true;

		}
		else
		{
			nextTime=Time.time+shootingSpeed;
			loading=false;
			cantShoot = false;
		}
		
		if(pauseMenu.GameIsPaused)
			return;
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
	
			 switch(touch.phase)	
			{
				//follow the finger
				case TouchPhase.Began:
					initialPosition = new Vector3(touch.position.x,touch.position.y,0);
					Camera.main.ScreenToWorldPoint(initialPosition);
					initialPosition.Normalize();
					aimPosition = new Vector3(touch.position.x,touch.position.y,0);
					fingerMoved=true;
				break;
				//follow the finger
				case TouchPhase.Moved:
					aimPosition = new Vector3(touch.position.x,touch.position.y,0);
					fingerMoved=true;
				break;
				//check if player can shoot and shoots
				case TouchPhase.Ended:
				if(transform.GetChild(0) != null )
				{
					if(!loading)
						shoot(touch);
				}
				break;
			} 
		}
	 		if(fingerMoved){
			aim();
		} 
	
	}
	
	void shoot(Touch touch) // shoots shape and loads the next one
	{
		transform.GetChild(0).GetComponent<Rigidbody2D>().isKinematic = false; // Rigidbody2D effect on
		Vector3 diff = Camera.main.ScreenToWorldPoint(aimPosition) - initialPosition;
		diff.Normalize();
		transform.GetChild(0).GetComponent<Rigidbody2D>().velocity=(diff)*30;
		transform.DetachChildren();
		GetComponent<shapeGenerator>().onCannonLoaded();
		loading=true;
		//predictionLine.changeOpacity();
		predictionLine.clearDots();
		fingerMoved = false;
	}
	
	void aim() // aims on touch
	{
		Vector3 diff = Camera.main.ScreenToWorldPoint(aimPosition) - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		if (transform.childCount > 0)
		{
			Vector3 diff_ = Camera.main.ScreenToWorldPoint(aimPosition) - initialPosition;
			diff_.Normalize();
			predictionLine.paintDotedLine(diff_ * 30, transform.GetChild(0).position);
		}

	}
	
}
