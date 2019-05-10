using UnityEngine;
using System.Collections;
 
public class DestroyOnAnimationFinish : MonoBehaviour {
	[SerializeField]
	private float delay = 0f;
	
	void Start () 
	{
		Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
	}
}