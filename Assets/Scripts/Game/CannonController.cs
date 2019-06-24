using UnityEngine;

public class CannonController : MonoBehaviour
{

	[SerializeField]
	private int power = 16;

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

	public void Shoot(Vector3 aimPosition) // shoots shape and loads the next one
	{
		Vector3 diff = aimPosition - transform.position;
		diff.Normalize();
		shapeGenerator.shape.GetComponent<Rigidbody2D>().isKinematic = false; // gravity effect on
		shapeGenerator.shape.transform.parent = shapesParent.transform;
		shapeGenerator.shape.GetComponent<Rigidbody2D>().velocity=(diff)*power;
		predictionLine.clearDots();
		PlayerStats.Instance.ReportProgress(1 , "shot");
		AudioManager.Instance.PlaySound(AudioManager.SoundName.cannonShot);
		DestroyShapes.NewShape(shapeGenerator.shape);
		shapeGenerator.LoadCannon();
		FireAnimation();
	}

	public void Aim(Vector3 aimPosition) // aims on touch
	{
		Vector3 diff = aimPosition - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		predictionLine.PaintDotedLine(diff * power, shapeGenerator.shape.transform.position);
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
