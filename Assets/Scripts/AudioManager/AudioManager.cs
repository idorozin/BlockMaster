using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public enum SoundName
	{
		blockLand,
		buttonClick,
		lava,
	}

	
	public static AudioManager Instance;

	[SerializeField]
	private Sound[] sounds;

	[SerializeField]
	private AudioSource soundSource;
	[SerializeField]
	private AudioSource musicSource;
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		SetSources();
	}

	void SetSources()
	{
		foreach (var s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
		}
	}

	private void Start()
	{
		PlaySound(SoundName.lava);
		PlaySound(SoundName.blockLand);
	}

	public void PlaySound(SoundName sound)
	{
		if (!PlayerStats.Instance.soundOn)
			return;
		Sound s = GetSound(sound);
		s.source.Play();
	}

	public Sound GetSound(SoundName sound)
	{
		foreach (var s in sounds)
		{
			if(s.name == sound)
				return s;
		}

		return null;
	}
}
