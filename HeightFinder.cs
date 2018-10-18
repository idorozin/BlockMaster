using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightFinder : MonoBehaviour {
	
	public static float height=0f;
	public static float score=0f;
	public static float lives=0;
	private Transform camera;
	Rigidbody2D rb;
	public GameObject surface;

	// Use this for initialization
	void Start ()
	{
		camera = GameObject.Find("Main Camera").transform;
		rb = GetComponent<Rigidbody2D>();
		height=0f;
		score=0f;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = new Vector2(0,-2);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.GetComponent<Rigidbody2D>() !=null && col.gameObject.GetComponent<Rigidbody2D>().velocity.x<0.1 && col.gameObject.GetComponent<Rigidbody2D>().velocity.y<0.1 && col.gameObject.GetComponent<Rigidbody2D>().velocity.x>-0.1 && col.gameObject.GetComponent<Rigidbody2D>().velocity.y>-0.1 && col.isTrigger==false){
			if (col.gameObject.name != surface.name && col.transform.position.y > surface.transform.position.y)
			{
				score=(float)Math.Round((col.transform.position.y-surface.transform.position.y)*10);
			}
			if(col.gameObject.name != surface.name && transform.position.y>0)
				height=col.transform.position.y;
		}
		this.rb.position = new Vector3(camera.position.x,camera.position.y+5f,camera.position.z);
	}
}






		/* Vector3 screenBottomCenter = new Vector3(Screen.width/2, Screen.height, 0);
		Vector3 inWorld = Camera.main.ScreenToWorldPoint(screenBottomCenter);
		if(rb.position.y < inWorld.y)
			rb.position = new Vector3(camera.position.x,camera.position.y+5f,camera.position.z); */