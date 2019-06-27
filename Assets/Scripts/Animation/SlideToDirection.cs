using System;
using System.Collections;
using UnityEngine;

public class SlideToDirection : MonoBehaviour {

	public void SlideToVector3(Vector3 desiredPosition)
	{
		StartCoroutine(SlideTo(desiredPosition));
	}

	[SerializeField] 
	private RuntimeAnimatorController animation;	
	[SerializeField] 
	private RuntimeAnimatorController startAnimation;
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float x, y, z;

	public bool anima = false;

	[SerializeField] private float smoothSpeed;
	[ContextMenu("slide")]
	public void SlideToVector3()
	{
		StartCoroutine(SlideTo(new Vector3(x , y , z)));
	}

	private IEnumerator SlideTo(Vector3 desiredPosition)
	{
		PauseMenu.GameIsPaused = true;
		if(anima)
			StartCoroutine(anim());
		TrackCamera.height = desiredPosition.y + 1f;
		while (desiredPosition.y - transform.position.y > 0.01f)
		{
			transform.position = Vector3.Lerp(transform.position, desiredPosition , smoothSpeed);
			yield return null;
		}
		PauseMenu.GameIsPaused = false;
		transform.position = desiredPosition;	
	}

	IEnumerator anim()
	{
		if (animation != null)
		{
			startAnimation = animator.runtimeAnimatorController;
			animator.runtimeAnimatorController = animation;
			yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
			animator.runtimeAnimatorController = startAnimation;
		}
	}

}
