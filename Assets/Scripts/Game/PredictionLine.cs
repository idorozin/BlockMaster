using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PredictionLine : MonoBehaviour {

	private Vector2 gravity=new Vector2(0f,-9f);
	private Vector2 launch_velocity;
	private Vector2 initial_position;
	private string dot_path="dot/dot_";
	private float time_step=0.2f;
	private int dot_num=20;
	private List<GameObject> dots = new List<GameObject>();

	private void Start()
	{
		GameObject dotPrefab = (GameObject)(Resources.Load(dot_path));
		GameObject PredictionLine_ = new GameObject("PredictionLine");
		Transform parent = PredictionLine_.transform;
		Color color;
		for(int i=0; i<dot_num;i++)
		{
				GameObject dot = Instantiate(dotPrefab , parent);
				color = new Color(1f, 1f, 1f,1f-(float)i*0.1f); // first dot 0% transparent -> last dot 99% transparent
				dot.GetComponent<SpriteRenderer>().color = color;
				dot.SetActive(false);
				dots.Add(dot);
		}
	}
		
		


	//instantiate the dots
	public void PaintDotedLine(Vector3 launch_velocity,Vector3 initial_position)
	{
		clearDots();
		
		Vector2 f_launch_velocity=new Vector2(launch_velocity.x,launch_velocity.y);
		Vector2 f_initial_position=new Vector2(initial_position.x,initial_position.y);
		int opacity = 0;
		for(int i=0; i<dot_num;i++)
		{
			if(-0.8f<CalculatePosition(f_launch_velocity,f_initial_position,time_step*i).y)
			{
				GameObject dot = dots[opacity];
				opacity++;
				dot.SetActive(true);
				dot.transform.position=CalculatePosition(f_launch_velocity,f_initial_position,time_step*i);
			}
		}
	}
	
	//calculate the dots position
	private Vector2 CalculatePosition(Vector2 LAUNCH_VELOCITY,Vector2 INITIAL_POSITION,float elapsedTime){
		 return gravity * elapsedTime * elapsedTime * 0.5f +
               LAUNCH_VELOCITY * elapsedTime + INITIAL_POSITION;
	}

	public void clearDots()
	{
		if (dots != null)
		{
			foreach (GameObject dot in dots)
				dot.SetActive(false);
		}
	}

	//TODO
	public void clearTouchingDots()
	{
		
	}
	

	public void ChangeOpacity()
	{
			if(dots!=null)
				foreach (GameObject dot_ in dots)
					if (dot_ != null)
						dot_.GetComponent<SpriteRenderer>().color = new Color(1f, 0.65f, 0.016f, .5f); // about 50% transparent
	}

	public Vector3 maxDot()
	{
		if (dots != null)
		{
			Vector3 maxdot;
			maxdot = dots[0].transform.position;
			foreach (GameObject dot_ in dots)
			{
				if (dot_ != null && dot_.transform.position.y > maxdot.y)
					maxdot = dot_.transform.position;
			}
			return maxdot;
		}

		return new Vector3(0f,0f,0f);
	}
}
