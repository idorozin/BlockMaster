using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dowloadUpdates : MonoBehaviour
{
	private string path="http://35.242.248.201/test.php";
     IEnumerator Start ()
     {
	     WWW www = new WWW(path);
	     yield return www;
	     Debug.Log("[ITS UPDATED]NEW UPDATES:" + www.text.ToString());
     }

}
