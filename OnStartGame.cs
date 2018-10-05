using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartGame : MonoBehaviour {
	Vector3 spawningPos;
	string cannonFilePath="";

	// Use this for initialization
	void Start () {
		spawningPos = new Vector3(transform.position.x,transform.position.y-3f,transform.position.z);
		if(PlayerStats.lastCannon=="")
			PlayerStats.lastCannon="default";
		cannonFilePath="Cannons/"+PlayerStats.lastCannon;
		Debug.Log(cannonFilePath);
		GameObject cannon = (GameObject)Instantiate(Resources.Load(cannonFilePath) , spawningPos , Quaternion.identity);
	}
	
}
