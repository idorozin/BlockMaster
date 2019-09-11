using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class DestroyShapes : MonoBehaviour
{

	private static int shapesCount = 0;
	private static int level = 0;
	private static int[] levels = new [] {5 , 7 , 10 , 12 , 15};


	private void Start()
	{
		shapesCount = 0;
		level = 0;
		GameManager.Instance.UpdateText(shapesCount + "/" + levels[level % (levels.Length)]);
	}

	public static void NewShape(GameObject obj)
	{
		shapesCount++;
		GameManager.Instance.UpdateText(Text());
		GameManager.Instance.shapes.Add(obj);
		if (shapesCount >= levels[level%(levels.Length)])
		{
			GameManager.Instance.surface_();
		}
	}

	public static string Text()
	{
		return shapesCount + "/" + levels[level % (levels.Length)];
	}

	public static void Destroyall()
	{
		foreach (var s in GameManager.Instance.shapes)
		{
			if (s != null)
			{
				s.GetComponent<SpriteRenderer>().sortingLayerName = "sky";
				s.GetComponent<Collider2D>().enabled = false;
				s.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			}
		}
		GameManager.Instance.shapes.Clear();
		level += 1;
		shapesCount = 0;
	}

}
