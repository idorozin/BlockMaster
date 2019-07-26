using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenuAttribute(menuName = "Rewards/Gold")]
public class Gold : Countable
{
	public override void Collect()
	{
		PlayerStats.Instance.gold += amount;
	}
}
