using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestroyShapes : MonoBehaviour
{

	private static int shapesCount = 0;
	private static int level = 0;
	private static int[] levels = new [] {5 , 7 , 10 , 12 , 15};
	public static float height=-1;

	[SerializeField]
	private TextMeshProUGUI text;

	private void Start()
	{
		height = -1f;
		shapesCount = 0;
		level = 0;
	}

	private void Update()
	{
		text.text = shapesCount + "/" + levels[level % (levels.Length)];
	}

	public static void NewShape(GameObject obj)
	{
		shapesCount++;
		GameManager.Instance.shapes.Add(obj);
		if (shapesCount >= levels[level%(levels.Length)])
		{
            GameManager.Instance.Surface();
            foreach (var s in GameManager.Instance.shapes)
			{
                s.GetComponent<Collider2D>().enabled = false;
                s.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			}
			level += 1;
			shapesCount = 0;
		}
	}

}
