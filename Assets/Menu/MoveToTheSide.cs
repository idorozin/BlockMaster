using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTheSide : MonoBehaviour
{

	[SerializeField]
	private float speed = 0.5f;

	void Update ()
	{
		transform.Translate(Vector3.right * Time.deltaTime*speed);
		Vector3 borders = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0f,0f));
		if (transform.position.x > borders.x + 1f)
			transform.position	= new Vector3(borders.x * -1 - 2f , transform.position.y , transform.position.z);

	}
}
