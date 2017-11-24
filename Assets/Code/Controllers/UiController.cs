using Assets.Code.Data.Lists;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Controllers
{
	public class UiController : MonoBehaviour {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {
			if (Input.GetKeyUp(KeyCode.Space))
			{
				if (Time.timeScale == 0)
					SpeedControlButtonClick();
				else
					PauseButtonClick();
			}

			var starDateText = GameObject.Find ("CurrentStarDate").GetComponent<Text>();
			starDateText.text = App.Instance.GameState.StarDate.ToString("yyyy-MM-dd-H");
			var moneyText = GameObject.Find ("MoneyText").GetComponent<Text>();
			moneyText.text = App.Instance.GameState.Money.ToString("c0");
			var pauseButtonText = GameObject.Find("PauseButton").GetComponentInChildren<Text>();
			pauseButtonText.text = Time.timeScale == 0 ? "Resume" : "Pause";
		    var buttonText = GameObject.Find("SpeedControlButton").GetComponentInChildren<Text>();
		    buttonText.text = string.Format("{0}x", Time.timeScale);
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
			if ((int)Time.timeScale != GameSpeeds.Paused) {
				App.Instance.PauseGame ();
			} else 
			{
				App.Instance.HandleSpeedChange ();
			}				
		}

		public void SpeedControlButtonClick()
		{
		    App.Instance.HandleSpeedChange();   
        }
	}
}

