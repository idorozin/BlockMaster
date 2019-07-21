using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
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
	private float dauration;
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
		GameObject notfi = Instantiate(c);
		notfi.GetComponent<ChallengeDisplay>().ShowCompleteChallenge(not);
		animationQueue.Enqueue(Animate(notfi));
		if (animateCoroutine == null)
			animateCoroutine = StartCoroutine(AnimateInOrder());
	}

	[ContextMenu("anim")]
	public void anim()
	{
		GameObject notfi = Instantiate(c);
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
		Debug.Log("animate");
		float screenHeight = canvas.GetComponent<RectTransform>().rect.height/2;
		float sizeY = notflication.GetComponent<RectTransform>().rect.height/2;
		Vector3 startPos = parent.localPosition;
		GameObject go = Instantiate(notflication, canvas.transform);
		go.transform.SetParent(parent);
		go.transform.localPosition = new Vector3(
			0f,
			screenHeight + sizeY,
			0f
		);
		Debug.Log(screenHeight + sizeY);
		parent.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,- (sizeY * 2)) , dauration);
		yield return new WaitForSecondsRealtime(dauration);
		yield return new WaitForSeconds(pauseTime);
		parent.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,startPos.y) , dauration);
		yield return new WaitForSecondsRealtime(dauration);
		Destroy(go);
		yield return new WaitForSecondsRealtime(0.1f);
		animateCoroutine = null;
		Destroy(notflication);
	}
}
