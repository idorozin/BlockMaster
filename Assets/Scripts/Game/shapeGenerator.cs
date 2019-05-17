using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShapeGenerator : MonoBehaviour 
{
	[SerializeField]
	private List<Shape> shapes;
	[SerializeField]
	private Transform spawningPos;
	[SerializeField]
	private int[] chanceArray;
	
	public GameObject shape;
	public Transform camera_;
	
	private void Start ()
	{
		ChanceArray();
		LoadCannon();
		camera_ = Camera.main.transform;
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

	private void Update () 
	{
		if(shape==null) LoadCannon();
		transform.position = new Vector3(0f,camera_.position.y-4f,0f);
	}

	public void LoadCannon()
	{
		//load the cannon with new shape
		shape = (GameObject) Instantiate(shapes[chanceArray[Random.Range(0, chanceArray.Length)]].prefab, spawningPos.position, transform.rotation);
		shape.transform.parent = transform;
	}
	
}
