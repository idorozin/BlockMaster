using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenuAttribute(menuName = "Rewards/Gold")]
public class Gold : Countable
{
	protected override void OnEnable()
	{
		base.OnEnable();
		icon = Resources.Load<Sprite>("CoinsLogo");
		faceColor = new Color32(255 , 238 ,0 ,255);
		outlineColor = new Color32(255 , 181 ,0 ,255);
	}

	public override void Collect()
	{
		PlayerStats.Instance.gold += amount;
	}
}
