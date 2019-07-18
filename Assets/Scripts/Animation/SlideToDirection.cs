using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
public class SlideToDirection : MonoBehaviour {

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
		transform.DOMove(new Vector3(x , y , z), 2, false).SetEase(Ease.InOutCubic);
		if(anima)
			StartCoroutine(anim());
	}

	public void SlideToVector3(Vector3 desiredPosition)
	{
		transform.DOMove(desiredPosition, 2, false).SetEase(Ease.InOutCubic).OnComplete(OnSlideEnd);
		if(anima)
			StartCoroutine(anim());
		GameManager.Instance.NextLevel = true;		
	}

	void OnSlideEnd()
	{
		GameManager.Instance.NextLevel = false;
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
