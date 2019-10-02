using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

	[SerializeField] private GameObject musicOn;
	[SerializeField] private GameObject musicOff;
	[SerializeField] private GameObject soundOn;
	[SerializeField] private GameObject soundOff;
	[SerializeField] private GameObject credits;
	[SerializeField] private GameObject settings;
	[Space]
	[SerializeField] private IAP noAds;


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

	public void LoadCredits()
	{
		settings.SetActive(false);
		credits.SetActive(true);
	}
	
	public void UnLoadCredits()
	{
		credits.SetActive(false);
		settings.SetActive(true);
	}
	
	public void Info()
	{
		Tutorial.closeTutorial = true;
		SceneManager.LoadScene("Tutorial");
	}

	public void NoAds()
	{
		if(PlayerStats.Instance.noAds)
			return;
		IAPManager.Instance.BuyProductById(noAds.ProductId);
	}

}
