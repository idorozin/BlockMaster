using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class NativeShare : MonoBehaviour {


	
	//TODO : NOT working on all android versions D:
	public void shareButtonPress()
	{
		if (!Application.isEditor)
		{	
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
				"Can you beat my score?");
			intentObject.Call<AndroidJavaObject>("setType", "text/plain");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
				intentObject, "Share your new score");
			currentActivity.Call("startActivity", chooser);
		}
	}

}
 