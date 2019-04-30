using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ShapeGenerator : MonoBehaviour 
{
	
	private List<string> shapeNames = new List<string>(){"box","box","box","box","box","box","box","box","box","box","box","box","h_rectangel","square","m_square","rectangel","circle_S"};
	[SerializeField]
	private List<Shape> shapes;
	private string filePath="Shapes/";
	public bool cannonLoaded=false;
	private Vector3 spawningPos;
	public Transform camera;
	private int circle = 0;
	[SerializeField]
	private int[] chanceArray;

	public static GameObject shape;
	
	//call the initialization method
	void Start ()
	{
		ChanceArray();
		camera=GameObject.Find("Main Camera").transform;
		cannonLoaded=false;
		spawningPos = new Vector3(transform.position.x,transform.position.y+1f,transform.position.z);
	}

	private void ChanceArray()
	{
		int sum = shapes.Sum(Shape => Shape.chance);
		chanceArray = new int[sum];
		int index=0 , shapePlace=0;
		foreach (var shape in shapes)
		{
			for (int i = 0; i < shape.chance; i++)
			{
				chanceArray[index] = shapePlace;
				index++;
			}

			shapePlace++;
		}
	}


	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(0f,camera.position.y-4f,0f);
		
		//load the cannon with new shape
		if(!cannonLoaded)
		{
			spawningPos = new Vector3(transform.position.x,transform.position.y+1f,transform.position.z);
			string prefabName=filePath+shapeNames[Random.Range(0,shapeNames.Count)];
			if (circle > 15)
			{
				prefabName = filePath + "circle_S";
				circle = 0;
			}
			shape = (GameObject)Instantiate(shapes[chanceArray[Random.Range(0,chanceArray.Length)]].prefab , spawningPos , Quaternion.identity); // todo transform.identity was pretty close but has centering isue
			shape.transform.parent=transform;
			cannonLoaded=true;
			circle++;
		}
	}
	
	public void onCannonLoaded(){
		cannonLoaded = !cannonLoaded;
	}
	

}
