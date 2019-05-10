using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ShapeGenerator : MonoBehaviour 
{
	
	[SerializeField]
	private List<Shape> shapes;
	public bool cannonLoaded=false;
	[SerializeField]
	private Transform spawningPos;
    [SerializeField]
    private Transform fireAnimPos;
    [SerializeField]
    private GameObject fireAnimation;
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
			Instantiate(fireAnimation , fireAnimPos.position , transform.rotation);
			if (circle > 15)
			{
				circle = 0;
			}
			shape = (GameObject)Instantiate(shapes[chanceArray[Random.Range(0,chanceArray.Length)]].prefab , spawningPos.position , transform.rotation); // todo transform.identity was pretty close but has centering isue
			shape.transform.parent=transform;
			cannonLoaded=true;
			
			circle++;
		}
	}
	
	public void onCannonLoaded(){
		cannonLoaded = !cannonLoaded;
	}
	

}
