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
		musicOn.SetActive(!PlayerStats.Instance.playerStats.musicOn);
		musicOff.SetActive(PlayerStats.Instance.playerStats.musicOn);
	}

	void changeSoundUi()
	{
		soundOn.SetActive(!PlayerStats.Instance.playerStats.soundOn);
		soundOff.SetActive(PlayerStats.Instance.playerStats.soundOn);
	}

	#endregion

	public void MusicOn()
	{
		PlayerStats.Instance.playerStats.musicOn = true;
		changeMusicUi();
		PlayerStats.Instance.saveFile();
		//set music on
	}

	public void MusicOff()
	{
		PlayerStats.Instance.playerStats.musicOn = false;
		changeMusicUi();
		PlayerStats.Instance.saveFile();
		//set music off
	}

	public void SoundOn()
	{
		PlayerStats.Instance.playerStats.soundOn = true;
		changeSoundUi();
		PlayerStats.Instance.saveFile();
		//set sound on
	}

	public void SoundOff()
	{
		PlayerStats.Instance.playerStats.soundOn = false;
		changeSoundUi();
		PlayerStats.Instance.saveFile();
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
