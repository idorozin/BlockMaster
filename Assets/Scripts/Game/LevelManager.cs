using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class LevelManager : MonoBehaviour
{

	private int shapesCount = 0;
	private int level = 0;
	[SerializeField]
	private int[] levels = new [] {5 , 7 , 10 , 12 , 15};

	
	[SerializeField] private GameObject surface;
	[SerializeField] private GameObject pigoom;


	private void Start()
	{
		shapesCount = 0;
		level = 0;
		UpdateText(shapesCount + "/" + levels[level % (levels.Length)]);
	}

	public void NewShape(GameObject obj)
	{
		shapesCount++;
		UpdateText(Text());
		GameManager.Instance.shapes.Add(obj);
		if (shapesCount >= levels[level%(levels.Length)])
		{
			NextLevel_();
		}
	}

	private void NextLevel_()
	{
		StartCoroutine(NextLevel());
	}
	
	private IEnumerator NextLevel()
	{
		GameManager.Instance.NextLevel = true;
		yield return new WaitForSeconds(2.7f);
		bool shapesMoving = true;
		int count = 0;
		while (shapesMoving)
		{
			shapesMoving = false;
			count++;
			foreach (var shape in GameManager.Instance.shapes)
			{
				count++;
				if (shape != null)
				{
					shapesMoving = !HeightFinder.IsNotMoving(shape.GetComponent<Rigidbody2D>());
					if (shapesMoving)
						break;
				}
			}
			yield return null;
		}
		if(count > 0)
			yield return new WaitForSeconds(0.5f);
		surface.GetComponent<SlideToDirection>().SlideToVector3(new Vector3(surface.transform.position.x, GameManager.Instance.height, surface.transform.position.z));
		Instantiate(pigoom, new Vector3(surface.transform.position.x, surface.transform.position.y + 0.905f, surface.transform.position.z),
			Quaternion.identity);
		FreezeCurrentShapes();
		UpdateText(Text());
	}
	
	private void FreezeCurrentShapes()
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

	
	[SerializeField]
	private TextMeshProUGUI shapesTillDestroy;
	
	private void UpdateText(string text)
	{
		shapesTillDestroy.text = text;
	}
	
	private string Text()
	{
		return shapesCount + "/" + levels[level % (levels.Length)];
	}


}
