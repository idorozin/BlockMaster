using UnityEngine;
using System.Collections;

public class ExtensionMethodHelper : MonoBehaviour
{
	private static ExtensionMethodHelper _Instance;

	public static ExtensionMethodHelper Instance
	{
		get
		{
			if (_Instance == null)
			{
				_Instance = new GameObject("ExtensionMethodHelper").AddComponent<ExtensionMethodHelper>();
			}

			return _Instance;
		}
	}
}

public static class ExtensionMethods
{
	public static void DisableAfterTimePassed(Challenge c)
	{
		ExtensionMethodHelper.Instance.StartCoroutine(DisableAfterTimePassed_(c));
		GameManager.GameOver += StopAllCoroutines;
	}

	public static void StopAllCoroutines()
	{
		ExtensionMethodHelper.Instance.StopAllCoroutines();
	}

	public static IEnumerator DisableAfterTimePassed_(Challenge c)
	{
		yield return new WaitForSeconds(c.timeToComplete);
		c.timePassed = true;
	}
}