using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

	[SerializeField] private GameObject musicOn;
	[SerializeField] private GameObject musicOff;
	[SerializeField] private GameObject soundOn;
	[SerializeField] private GameObject soundOff;


	#region setUI

	void SetUI()
	{
		changeMusicUi();
		changeSoundUi();
	}

	void changeMusicUi()
	{
		musicOn.SetActive(!PlayerStats.Instance.musicOn);
		musicOff.SetActive(PlayerStats.Instance.musicOn);
	}

	void changeSoundUi()
	{
		soundOn.SetActive(!PlayerStats.Instance.soundOn);
		soundOff.SetActive(PlayerStats.Instance.soundOn);
	}

	#endregion

	public void MusicOn()
	{
		PlayerStats.Instance.musicOn = true;
		changeMusicUi();
		PlayerStats.saveFile();
		//set music on
	}

	public void MusicOff()
	{
		PlayerStats.Instance.musicOn = false;
		changeMusicUi();
		PlayerStats.saveFile();
		//set music off
	}

	public void SoundOn()
	{
		PlayerStats.Instance.soundOn = true;
		changeSoundUi();
		PlayerStats.saveFile();
		//set sound on
	}

	public void SoundOff()
	{
		PlayerStats.Instance.soundOn = false;
		changeSoundUi();
		PlayerStats.saveFile();
		//set sound off
	}

	public void OnLoadSettings()
	{
		gameObject.SetActive(true);
		SetUI();
	}
	
	

	public void Credits()
	{
	}
	
	public void Info()
	{
	}

}
