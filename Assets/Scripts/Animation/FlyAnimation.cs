using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAnimation : MonoBehaviour {
	
	[SerializeField]
	float m_Speed= 1  ;
	[SerializeField]
	float m_XScale= 1 ;
	[SerializeField]
	float m_YScale= 1 ;
 
	private Vector3 m_Pivot;
	private Vector3 m_PivotOffset;
	[SerializeField]
	private float m_Phase ;
	private bool m_Invert;
	private float  m_2PI= Mathf.PI * 2;

	[SerializeField]
	private float speedModifier = 1;

	[SerializeField]
	private float speed = 1;
 
	void Start() {
		m_Pivot = transform.position;
	}
 
	void Update ()
	{
		speedModifier++;
		m_Speed = speed * (float)Math.Abs(Math.Sin(speedModifier*2));
		m_PivotOffset = Vector3.up * 2 * m_YScale;
		m_Phase += m_Speed * Time.deltaTime;
		if(m_Phase > m_2PI)
		{
			m_Invert = !m_Invert;
			m_Phase -= m_2PI;
		}
		if(m_Phase < 0) m_Phase += m_2PI;
   
		transform.position = m_Pivot + (m_Invert ? m_PivotOffset : Vector3.zero);
		transform.position += new Vector3( Mathf.Sin(m_Phase) * m_XScale,
		 Mathf.Cos(m_Phase) * (m_Invert ? -1 : 1) * m_YScale , 0f);
	}

}
