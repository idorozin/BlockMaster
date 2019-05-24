
using UnityEngine;

public class SetSkin : MonoBehaviour
{
	private SpriteRenderer sprite;
	void Start ()
	{
		sprite = GetComponent<SpriteRenderer>();
		if(Resources.Load<Sprite>(PlayerStats.Instance.lastCannon)!=null)
			sprite.sprite = Resources.Load<Sprite>(PlayerStats.Instance.lastCannon);
	}
}
