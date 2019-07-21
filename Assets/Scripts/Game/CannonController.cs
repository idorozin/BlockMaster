using System;
using UnityEngine;

public class CannonController : MonoBehaviour
{

	[SerializeField]
	private float power = 30;

	[SerializeField] [Range(0, 1)] private float Ysesitivity = 1;
	[SerializeField] [Range(0, 1)] private float Xsesitivity = 1;
	

	[SerializeField] private float maxY = 0.75f;

	private PredictionLine predictionLine;
	private ShapeGenerator shapeGenerator;
	private CameraShake cameraShake;
	private GameObject shapesParent;

	
	private void Awake()
	{
		predictionLine = GetComponent<PredictionLine>();
		shapeGenerator = GetComponent<ShapeGenerator>();
		shapesParent = new GameObject("Shapes");
	}

	public void Shoot(Vector3 basePosition,Vector3 aimPosition) // shoots shape and loads the next one
	{
		Vector3 diff = aimPosition - basePosition;
		diff *= power;
		diff = new Vector3(diff.x*Xsesitivity , Math.Min(diff.y,maxY)*Ysesitivity,0);
		//diff.Normalize();
		shapeGenerator.shape.GetComponent<Rigidbody2D>().isKinematic = false; // gravity effect on
		shapeGenerator.shape.transform.parent = shapesParent.transform;
		shapeGenerator.shape.GetComponent<Rigidbody2D>().velocity=(diff);
		predictionLine.clearDots();
		PlayerStats.Instance.ReportProgress(1 , shapeGenerator.shape.name);
		PlayerStats.Instance.ReportProgress(1 , "shot");
		AudioManager.Instance.PlaySound(AudioManager.SoundName.cannonShot);
		DestroyShapes.NewShape(shapeGenerator.shape);
		shapeGenerator.LoadCannon();
		FireAnimation();
	}

	public void Aim(Vector3 basePosition,Vector3 aimPosition) // aims on touch
	{
		Vector3 diff = aimPosition - basePosition;
		//Debug.Log("aimPosition " + aimPosition + " basePosition" +basePosition);
		//diff.Normalize();
		diff *= power;
		diff = new Vector3(diff.x*Xsesitivity , Math.Min(diff.y,maxY)*Ysesitivity,0);
		//diff = new Vector3(diff.x , Math.Min(diff.y,maxY),0f);
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		predictionLine.PaintDotedLine(diff, shapeGenerator.shape.transform.position);
	}
	
	
	[SerializeField]
	private Transform fireAnimPos;
	[SerializeField]
	private GameObject fireAnimation;
	
	private void FireAnimation()
	{
		GameObject anim = Instantiate(fireAnimation, fireAnimPos.position, transform.rotation);
		anim.transform.parent = transform;
	}

}
