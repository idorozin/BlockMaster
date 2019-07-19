using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTheSide : MonoBehaviour
{
	private static Camera camera;
	private Vector3 borders;
	[SerializeField][Range(-3,3)]
	private float speed = 0.5f;
	[SerializeField] 
	private float time = 1f;

	private float height;
	private void Start()
	{
		if (camera == null) camera = Camera.main;
		borders = camera.ScreenToWorldPoint(new Vector3(Screen.width,0f,0f));
		Debug.Log(borders);
		height = transform.position.y;
	}

	void Update ()
	{
		transform.Translate(Vector3.right * Time.deltaTime * speed);
		if (speed > 0 && transform.position.x > borders.x + time)
			transform.position	= new Vector3(borders.x * -1 - 2f , height , transform.position.z);
		else if(speed < 0 && transform.position.x < -borders.x - time)
			transform.position	= new Vector3(borders.x * 1 + 2f , height , transform.position.z);
	}
}
