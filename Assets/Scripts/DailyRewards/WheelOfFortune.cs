﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WheelOfFortune : MonoBehaviour
{
	private WheelJoint2D wheelJoint;
	private JointMotor2D motor;
	public static bool rollAllowed=true;
	[SerializeField]
	private int startSpeed=600, stopSpeed=50 , prizeIndex;	
	[SerializeField]
	private RewardDialog rewardDialog;
	[SerializeField]
	private Reward_[] rewards;
	
	private void Awake()
	{
		wheelJoint = GetComponent<WheelJoint2D>();
		motor = new JointMotor2D();
	}

	public void OnMouseDown()
	{
		if (!rollAllowed || !DailyReward.RollAllowed)
			return;
		if (Application.internetReachability == NetworkReachability.NotReachable)
			return;
		startSpeed = UnityEngine.Random.Range(400 , 800);
		stopSpeed = UnityEngine.Random.Range(50 ,100);
		motor.motorSpeed = startSpeed;
		motor.maxMotorTorque = 10000;
		wheelJoint.motor = motor;
		DailyReward.RollAllowed = false;
		GameObject.Find("TimeManager").GetComponent<DailyReward>().StartCoroutine("ResetTimer");
		StartCoroutine(roll(startSpeed));
	}

	public void Rol()
	{
		StartCoroutine(roll(startSpeed));
	}

	private IEnumerator roll(int startSpeed)
	{
		rollAllowed = false;
		for (int i = startSpeed; i > 0; i=i-stopSpeed/2)
		{
			motor.motorSpeed = i;
			motor.maxMotorTorque = 10000;
			wheelJoint.motor = motor;
			yield return new WaitForSeconds(0.5f);
		}

		for (int i = (int)motor.motorSpeed; i > 0; i-=10)
		{
			motor.motorSpeed = i;
			motor.maxMotorTorque = 10000;
			wheelJoint.motor = motor;
			yield return new WaitForSeconds(0.5f);
		}
		
		for (int i = (int)motor.motorSpeed; i > 0; i--)
		{
			motor.motorSpeed = i;
			motor.maxMotorTorque = 10000;
			wheelJoint.motor = motor;
			yield return new WaitForSeconds(0.5f);
		}
		motor.motorSpeed = 0;
		motor.maxMotorTorque = 10000;
		wheelJoint.motor = motor;
		prizeIndex = (int)Math.Round(transform.GetChild(0).eulerAngles.z);
		prizeIndex=getPrizeIndex(prizeIndex);
		getPrize(prizeIndex);
		rollAllowed = true;
	}
	
	int getPrizeIndex(int angel)
	{
		Debug.Log(angel);
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

	void getPrize(int prizeIndex)
	{
		RewardDialog r = Instantiate(rewardDialog);
		r.CollectPrizeWithAnimation(rewards[prizeIndex]);
		PlayerStats.saveFile();
	}
	
}
