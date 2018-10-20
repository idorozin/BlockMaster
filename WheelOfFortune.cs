using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOfFortune : MonoBehaviour
{
	private WheelJoint2D wheelJoint;
	private JointMotor2D motor;
	private bool rollAllowed=true;
	[SerializeField]
	private int startSpeed = 1000;
	
	private void Awake()
	{
		wheelJoint = GetComponent<WheelJoint2D>();
		motor = new JointMotor2D();
	}

	private void OnMouseDown()
	{
		Debug.Log("MouseDowns");
		if (!rollAllowed)
			return;
		Debug.Log("roll");
		motor.motorSpeed = startSpeed;
		motor.maxMotorTorque = 10000;
		wheelJoint.motor = motor;
		StartCoroutine(roll(startSpeed));
	}

	private IEnumerator roll(int startSpeed)
	{
		Debug.Log("corrotine");
		rollAllowed = false;
		for (int i = startSpeed; i > 0; i=i-100)
		{
			motor.motorSpeed = i;
			motor.maxMotorTorque = 10000;
			wheelJoint.motor = motor;
			yield return new WaitForSeconds(1f);
		}

		motor.motorSpeed = 10;
		motor.maxMotorTorque = 10000;
		wheelJoint.motor = motor;
		yield return new WaitForSeconds(1f);
		motor.motorSpeed = 0;
		motor.maxMotorTorque = 10000;
		wheelJoint.motor = motor;
		
		rollAllowed = true;
	}
}
