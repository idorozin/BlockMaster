﻿using System.Collections.Generic;
using UnityEngine;

public class shapeGenerator : MonoBehaviour 
{
	
	private List<string> shapes;
	private string filePath="Shapes/";
	private bool cannonLoaded=false;
	private Vector3 spawningPos;
	public Transform camera;
	private int circle = 0;

	//initialize the list of the shapes
	private List<string> shapesList()
	{
		List<string> shapes=new List<string>();
		string[] shapeNames={"box","box","box","box","box","box","box","box","box","box","box","box","h_rectangel","square","m_square","rectangel","circle_S"};	
		foreach (string shape in shapeNames)
			shapes.Add(shape);
		return shapes;
	}
	
	//call the initialization method
	void Start () 
	{
		camera=GameObject.Find("Main Camera").transform;
		shapes=shapesList();
		cannonLoaded=false;
		spawningPos = new Vector3(transform.position.x,transform.position.y+1f,transform.position.z);
	}


	// Update is called once per frame
	void Update () 
	{
		transform.position= new Vector3(0f,camera.position.y-4f,0f);
		if(shapes==null || shapes.Count==0)
		{
			shapes=shapesList();
		}
		
		//load the cannon with new shape
		if(!cannonLoaded)
		{
			spawningPos = new Vector3(transform.position.x,transform.position.y+1f,transform.position.z);
			string prefabName=filePath+shapes[Random.Range(0,shapes.Count)];
			if (circle > 15)
			{
				prefabName = filePath + "circle_S";
				circle = 0;
			}
			GameObject shape = (GameObject)Instantiate(Resources.Load(prefabName) , spawningPos , Quaternion.identity); // todo transform.identity was pretty close but had centering isue
			shape.transform.parent=transform;
			cannonLoaded=true;
			circle++;
		}
	}
	
	public void onCannonLoaded(){
		cannonLoaded = !cannonLoaded;
	}
	
	//spawn the cannon

}
