using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Code.BaseClasses;
using Data.Lists;

namespace Code.UserInterface
{
	public class UiController : MonoBehaviour {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {
			var starDateText = GameObject.Find ("CurrentStarDate").GetComponent<Text>();
			starDateText.text = App.Instance.GameState.StarDate.ToString("yyyy-MM-dd-H");
			var moneyText = GameObject.Find ("MoneyText").GetComponent<Text>();
			moneyText.text = App.Instance.GameState.Money.ToString("c0");
		}


		public void MainMenuButtonClick()
		{
			App.Instance.LoadMainMenu ();
		}

		public void SystemViewButtonClick()
		{
			App.Instance.LoadScene (SceneNames.SystemView);
		}

		public void PauseButtonClick()
		{
			var buttonText = GetComponentInChildren<Text> ();
			if ((int)Time.timeScale != GameSpeeds.Paused) {
				App.Instance.PauseGame ();
				buttonText.text = "Resume";
			} else 
			{
				App.Instance.HandleSpeedChange ();
				buttonText.text = "Pause";
			}				
		}

		public void SpeedControlButtonClick()
		{
			var buttonText = GetComponentInChildren<Text> ();
			App.Instance.HandleSpeedChange ();
			buttonText.text = string.Format ("{0}x", Time.timeScale);
		}
	}
}

