﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public enum SoundName
	{
		blockLand,
		buttonClick,
		cannonShot,
		ChallengeComplete,
		coins,
		DiamondWin,
		NewRecord,
		Purchase
	}
	
	public static AudioManager Instance;

	[SerializeField]
	private Sound[] sounds;
	
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}
		SetSources();
	}

	private void SetSources()
	{
		foreach (var s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
		}
	}

	public void PlaySound(SoundName sound)
	{
		if (!PlayerStats.Instance.soundOn)
			return;
		Sound s = GetSound(sound);
		s?.source.Play();
	}
	
	
	public void StopSound(SoundName sound)
	{
		Sound s = GetSound(sound);
		s?.source.Stop();
	}

	private Sound GetSound(SoundName sound)
	{
		foreach (var s in sounds)
		{
			if(s.name == sound)
				return s;
		}

		return null;
	}
}
