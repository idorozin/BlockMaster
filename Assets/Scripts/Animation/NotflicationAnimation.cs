using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class NotflicationAnimation : MonoBehaviour
{
	[SerializeField] 
	private GameObject canvas;
	[SerializeField] 
	private Transform parent;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float step;
	[SerializeField] 
	private float timeTillDestroy = 1f;
	[SerializeField] 
	private float pauseTime = 1f;
	[SerializeField]
	private GameObject c;

	private Coroutine animateCoroutine;
	
	private new Camera camera;

	private Queue<IEnumerator> animationQueue = new Queue<IEnumerator>();
	
	void Start ()
	{
		camera = Camera.main;
	}

	public void animate(Challenge not)
	{
		Debug.Log(not.description);
		GameObject notfi = Instantiate(c);
		notfi.GetComponent<ChallengeDisplay>().ShowCompleteChallenge(not);
		animationQueue.Enqueue(Animate(notfi));
		if (animateCoroutine == null)
			animateCoroutine = StartCoroutine(AnimateInOrder());
	}
	
	private IEnumerator AnimateInOrder ()
	{
		while (animationQueue.Any())
		{
			yield return animationQueue.Dequeue();
		}
		animateCoroutine = null;
	}

	private IEnumerator Animate (GameObject notflication)
	{
		float screenHeight = canvas.GetComponent<RectTransform>().rect.height/2;
		float sizeY = notflication.GetComponent<RectTransform>().rect.height/2;
		Vector3 startPos = parent.localPosition;
		GameObject go = Instantiate(notflication , canvas.transform);
		go.transform.localPosition = new Vector3(
			0f,
			screenHeight + sizeY,
			0f
		);
		while (go.transform.localPosition.y >
		       screenHeight - sizeY)
		{
			parent.localPosition = parent.localPosition + Vector3.down *  step;
			go.transform.localPosition = go.transform.localPosition + Vector3.down * step;
			yield return new WaitForSeconds(speed);
		}
		Destroy(go , timeTillDestroy);
		yield return new WaitForSeconds(pauseTime);
		while (parent.localPosition.y < startPos.y)
		{
			if(go!=null)
				go.transform.localPosition = go.transform.localPosition + Vector3.up * step;
			parent.localPosition = parent.localPosition + Vector3.up * step;
			yield return new WaitForSeconds(speed);
		}
		animateCoroutine = null;
		Destroy(notflication);
	}
}
