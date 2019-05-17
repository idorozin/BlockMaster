using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackCamera : MonoBehaviour
{

	public float trackingSpeed = 0.1f;
	public float offset = 0;
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance.height < 0)
			return;
		 if(GameManager.Instance.height > transform.position.y+0.1f)
			 transform.position = new Vector3(transform.position.x,transform.position.y+trackingSpeed,transform.position.z);
		 if(GameManager.Instance.height < transform.position.y-0.1f)
			 transform.position = new Vector3(transform.position.x,transform.position.y-trackingSpeed,transform.position.z);
		
	}
}
