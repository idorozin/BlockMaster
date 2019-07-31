using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class NativeShare : MonoBehaviour
{

	[SerializeField]
	private string textBeforeScore = "#BlockMaster My best score is ";
	[SerializeField]
	private string textAfterScore = " Can you beat me? ";
	[SerializeField]
	private string applink;	
	[SerializeField]
	private string chooserText = "Share your high score";
	
	public void ShareButtonPress()
	{
		if (!Application.isEditor)
		{	
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
				textBeforeScore + PlayerStats.Instance.highScore + textAfterScore + applink);
			intentObject.Call<AndroidJavaObject>("setType", "text/plain");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
				intentObject, chooserText);
			currentActivity.Call("startActivity", chooser);
		}
	}

}
 