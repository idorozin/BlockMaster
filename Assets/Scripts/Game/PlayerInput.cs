using UnityEngine;

public class PlayerInput : MonoBehaviour
{

	private Vector3 initialPosition, aimPosition;
	bool fingerMoved, loading;
	float nextTime;
	public float shootingSpeed = 0.5f;
	PredictionLine predictionLine;
	private Camera camera_;
	private CannonController cannonController;
	private bool began;


	private void Awake()
	{
		cannonController = GetComponent<CannonController>();
	}

	private void Start()
	{
		camera_ = Camera.main;
	}

	private void Update()
	{
		if (PauseMenu.GameIsPaused) return;

		if (Time.time > nextTime)
		{
			nextTime = Time.time + shootingSpeed;
			loading = false;
		}

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			switch (touch.phase)
			{
				//follow the finger
				case TouchPhase.Began:
					initialPosition = GetWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
					initialPosition.Normalize();
					aimPosition = GetWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
					fingerMoved = true;
					began = true;
					break;
				//follow the finger
				case TouchPhase.Moved:
					aimPosition = GetWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
					fingerMoved = true;
					break;
				//check if player can shoot and shoots
				case TouchPhase.Ended:
					if (!loading && began)
					{
						cannonController.Shoot(aimPosition);
						loading = true;
						fingerMoved = false;
						began = false;
					}

					break;
			}
		}

		if (fingerMoved)
			cannonController.Aim(aimPosition);
	}

	private Vector3 GetWorldPoint(Vector3 position)
	{
		return camera_.ScreenToWorldPoint(position);
	}

	[ContextMenu("report")]
	public void Report()
	{
		PlayerStats.Instance.ReportProgress(5 , "shot");
	}


}
