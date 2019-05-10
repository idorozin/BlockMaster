using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {
	[SerializeField]
	private float speed = 10f;
    
    
	void Update ()
	{
		RectTransform rectTransform = GetComponent<RectTransform>();
		rectTransform.Rotate( new Vector3( 0, 0, speed ) );
	}
}
