using System;
using System.Collections;
using UnityEngine;

public class SlideToDirection : MonoBehaviour {

	public void SlideToVector3(Vector3 desiredPosition)
	{
		StartCoroutine(SlideTo(desiredPosition));
	}

	[SerializeField]
	private float x, y, z;

	[SerializeField] private float smoothSpeed;
	[ContextMenu("slide")]
	public void SlideToVector3()
	{
		StartCoroutine(SlideTo(new Vector3(x , y , z)));
	}

	private IEnumerator SlideTo(Vector3 desiredPosition)
	{
		while (desiredPosition.y - transform.position.y > 0.01f)
		{
			transform.position = Vector3.Lerp(transform.position, desiredPosition , smoothSpeed);
			yield return null;
		}
		transform.position = desiredPosition;
		
	}
	
}
