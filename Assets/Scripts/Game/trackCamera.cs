using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCamera : MonoBehaviour
{

	public float trackingSpeed = 0.1f;
	public float offset = 0;

    public static float height = 0f;
	// Update is called once per frame
	void Update ()
	{
		if (height < 0)
			return;
		 if(height > transform.position.y+0.1f)
			 transform.position = new Vector3(transform.position.x,transform.position.y+trackingSpeed,transform.position.z);
		 if(height < transform.position.y-0.1f)
			 transform.position = new Vector3(transform.position.x,transform.position.y-trackingSpeed,transform.position.z);
		
	}
}
