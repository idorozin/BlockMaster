using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackCamera : MonoBehaviour
{

	public float trackingSpeed = 0.1f;
	
	// Update is called once per frame
	void Update ()
	{
		if (HeightFinder.height < 0)
			return;
		 if(HeightFinder.height > transform.position.y+0.1f)
			 transform.position = new Vector3(transform.position.x,transform.position.y+trackingSpeed,transform.position.z);
		 if(HeightFinder.height < transform.position.y-0.1f)
			 transform.position = new Vector3(transform.position.x,transform.position.y-trackingSpeed,transform.position.z);
		
	}
}
