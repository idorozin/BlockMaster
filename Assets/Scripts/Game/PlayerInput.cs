using UnityEngine;

public class PlayerInput : MonoBehaviour
{

	private Vector3 initialPosition, aimPosition;
	private Vector3 cameraInitialPosition;
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
		if (GameManager.Instance != null&& PauseMenu.GameIsPaused || GameManager.Instance.NextLevel) return;

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
					cameraInitialPosition = camera_.transform.position;
					aimPosition = GetWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
					fingerMoved = true;
					began = true;
					break;
				//follow the finger
				case TouchPhase.Moved:
					if(!began)
						break;
					aimPosition = GetWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
					fingerMoved = true;
					break;
				//check if player can shoot and shoots
				case TouchPhase.Ended:
					if (!loading && began)
					{
						cannonController.Shoot(initialPosition,aimPosition);
						loading = true;
						fingerMoved = false;
						began = false;
					}

					break;
			}
		}

		if (fingerMoved)
			cannonController.Aim(initialPosition,aimPosition);
	}

	private Vector3 diff;
	private Vector3 GetWorldPoint(Vector3 position)
	{
		diff = camera_.transform.position - cameraInitialPosition;
		return camera_.ScreenToWorldPoint(position) - diff;
	}

	[ContextMenu("report")]
	public void Report()
	{
		PlayerStats.Instance.ReportProgress(5 , "shot");
	}
	[ContextMenu("reportRect")]
	public void Reportrect()
	{
		PlayerStats.Instance.ReportProgress(5 , "Rectangle");
	}
	[ContextMenu("reportreach")]
	public void Reportreach()
	{
		PlayerStats.Instance.ReportProgress(100 , "score");
	}


}
