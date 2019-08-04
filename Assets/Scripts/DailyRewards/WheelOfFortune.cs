using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class WheelOfFortune : MonoBehaviour
{
	[SerializeField]
	private int startSpeed=600, stopSpeed=50 , prizeIndex;	
	[SerializeField]
	private RewardDialog rewardDialog;
	[SerializeField]
	private Reward_[] rewards;

	public static bool rollAllowed = true;
	public bool testing;

	[SerializeField] private ActivateWheelOfFortune dailyReward;

	[SerializeField] private GameObject offline;

	public void OnMouseDown()
	{
		if (!rollAllowed)
			return;
		if (!testing)
		{
			if (!ActivateWheelOfFortune.RollAllowed)
				return;
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				offline.SetActive(true);
				return;
			}

			if (dailyReward.validationRequired)
			{
				
			}
			startSpeed = UnityEngine.Random.Range(400, 800);
			stopSpeed = UnityEngine.Random.Range(50, 100);
			ActivateWheelOfFortune.RollAllowed = false;
			StartCoroutine(dailyReward.ResetTimer());
		}
		StartCoroutine(roll(UnityEngine.Random.Range(0 ,360)));
	}

	public void Rol()
	{
		StartCoroutine(roll(startSpeed));
	}

	public int fullCircles=4;
	private float _currentLerpRotationTime;
	private float maxLerpRotationTime;
	public Transform Circle;
	private float _finalAngle = 1000;
	private float _startAngle=0;

	private IEnumerator roll(float randomAngle)
	{
		_finalAngle = randomAngle + (360*fullCircles);	
		rollAllowed = false;
		float _currentLerpRotationTime = 0f;
		while (true)
		{
			float maxLerpRotationTime = 4f;
			// increment timer once per frame
			_currentLerpRotationTime += Time.deltaTime;
			if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle) {
				_currentLerpRotationTime = maxLerpRotationTime;
				_startAngle = _finalAngle % 360;
				break;
			}
			// Calculate current position using linear interpolation
			float t = _currentLerpRotationTime / maxLerpRotationTime;
			// This formulae allows to speed up at start and speed down at the end of rotation.
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			float angle = Mathf.Lerp (_startAngle, _finalAngle, t);
			Circle.transform.eulerAngles = new Vector3 (0, 0, angle);
			yield return null;
		}
		rollAllowed = true;
		prizeIndex = GetPrizeByAngle((int)randomAngle);
		GetPrize(prizeIndex);
	}

	int GetPrizeByAngle(int angel)
	{
		if (angel >= 0 && angel < 47)
			return 0;
		if (angel >= 47 && angel < 100)
			return 1;
		if (angel >= 100 && angel < 139)
			return 2;
		if (angel >= 139 && angel < 180)
			return 3;
		if (angel >= 180 && angel < 227)
			return 4;
		if (angel >= 227 && angel < 270)
			return 5;
		if (angel >= 270 && angel < 320)
			return 6;
		if (angel >= 320 && angel <= 360)
			return 7;
		return -999;
	}

	private void GetPrize(int prizeIndex)
	{
		RewardDialog r = Instantiate(rewardDialog);
		r.CollectPrizeWithAnimation(rewards[prizeIndex]);
		PlayerStats.saveFile();
	}
	
}
