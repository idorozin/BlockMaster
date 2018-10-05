using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackCamera : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		 if(HeightFinder.height > transform.position.y+0.2f)
			 transform.position = new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z);
		 if(HeightFinder.height < transform.position.y-0.2f)
			 transform.position = new Vector3(transform.position.x,transform.position.y-0.1f,transform.position.z);
		
	}
}
