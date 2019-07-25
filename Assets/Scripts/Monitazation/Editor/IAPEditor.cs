using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(IAP))]
public class IAPEditor : Editor
{
	private GameObject go;
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		go = (GameObject) EditorGUILayout.ObjectField("Preview",go,typeof(GameObject));
		IAP iap = (IAP) target;

		if ( GUILayout.Button("Update") || (iap.size==Vector2.zero && iap.position==Vector2.zero && go!=null))
		{
			iap.size = go.GetComponent<RectTransform>().sizeDelta;
			iap.position = go.GetComponent<RectTransform>().anchoredPosition;
			iap.Image = go.GetComponent<Image>().sprite;
		}
	}
}
