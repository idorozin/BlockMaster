using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShapes : MonoBehaviour
{

	public static List<GameObject> shapes = new List<GameObject>();
	private static int shapesCount = 0;
	private static int level = 0;
	[SerializeField]
	private static int[] levels = new [] {5 , 7 , 10 , 12 , 15};
	public static float height=-1;

	private void Start()
	{
		height = -1f;
		shapesCount = 0;
		level = 0;
	}

	public static void NewShape(GameObject obj)
	{
		shapesCount++;
		shapes.Add(obj);
		if (shapesCount > levels[level%(levels.Length)])
		{
			height = GameManager.height;
			foreach (var s in shapes)
			{
				Destroy(s);
			}
			level += 1;
			shapesCount = 0;
		}
	}

}
