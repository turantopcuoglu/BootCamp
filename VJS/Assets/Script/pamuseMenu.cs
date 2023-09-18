using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pamuseMenu : MonoBehaviour
{
	private const float ZAMAN_BASLA = 3f;
	private float startTime2;

	private static pamuseMenu instance;
	public static pamuseMenu Instate { get { return instance; } }

	[SerializeField] private GameObject Panel;
	[SerializeField] private float goldTime, silverTime, startTime;

	[SerializeField] private float lvlDuration;
	[SerializeField] private Text timeText;


	void Start()
	{
		instance = this;
		startTime = Time.time;
		Panel.SetActive(false);
	}

	void Update()
	{
		if (Time.time - startTime2 < ZAMAN_BASLA)
			return;

		lvlDuration = Time.time - (startTime + ZAMAN_BASLA);
		string minutes = ((int)lvlDuration / 60).ToString("00");
		string seconds = ((int)lvlDuration % 60).ToString("00");

		timeText.text = minutes + ":" + seconds;
	}

	public void pauseMenu()
	{
		Panel.SetActive(!Panel.activeSelf);
		if (!Panel.activeSelf)
		{
			Time.timeScale = 1;
		}
		else
		{
			Time.timeScale = 0;
		}
	}

	public void resumeMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1;
	}
	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}
	public void exit()
	{
		Application.Quit();
	}

	public void Victory()
	{
		float duration = Time.time - (startTime + ZAMAN_BASLA);
		if (duration < goldTime)
		{
			GameManager.Instance.simdiki += 50;
		}
		else if (duration < silverTime)
		{
			GameManager.Instance.simdiki += 25;
		}
		else
		{
			GameManager.Instance.simdiki += 5;
		}
		GameManager.Instance.Save();
		string saveString = "";

		LevelData level = new LevelData(SceneManager.GetActiveScene().name);
		saveString += (level.BestTime > duration || level.BestTime == 0.0f) ? duration.ToString() : level.BestTime.ToString();


		saveString += '&';
		saveString += silverTime.ToString();
		saveString += '&';
		saveString += goldTime.ToString();
		saveString += '&';
		PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);
		SceneManager.LoadScene(0);


	}
}
