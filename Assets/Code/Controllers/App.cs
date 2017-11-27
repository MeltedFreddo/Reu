using System.Linq;
using Assets.Code.Data;
using Assets.Code.Data.Lists;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Controllers
{
	public class App : MonoBehaviour
	{
	
		private static App _singleton;

		public static App Instance { get { return _singleton; } }

		private string _currentSceneName;
        
        public Data.GameState GameState;
		public Data.Model Model;

	    public Colony CurrentColony { get; set; }


		void Awake()
		{
			if (_singleton != null)
			{	
				Debug.LogError("App Singleton pattern violated");
			}

			_singleton = this;

			// Are we in the Application Scene?
			if (SceneManager.GetActiveScene().name == "Application")
			{
				// Make sure this object persists between scene loads.
				DontDestroyOnLoad(gameObject);

				LoadMainMenu();
			}
			else
			{
				// This is a temporary instance, create dummy game data
				AssembleNewGameState();
			}
		}

		// Use this for initialization
		void Start()
		{	
			
		}
	
		// Update is called once per frame
		void Update()
		{
		
		}

		private void AssembleNewGameState()
		{
			GameState = new Data.GameState();
			Model = new Data.Model();
			Time.timeScale = 1;
			InvokeRepeating("IncrementStarDate", 0, 1);
		}

		public void StartNewGame()		
		{
			AssembleNewGameState();
			LoadScene(SceneNames.SystemView);
		}

		public void Quit()
		{
			Debug.Log("App: Terminating.");

			Application.Quit();
		}

		public void LoadScene(string sceneName)
		{
			if (SceneManager.GetActiveScene().name != sceneName)
			{
				Debug.Log("App: Loading Scene, " + sceneName);

				_currentSceneName = sceneName;

				SceneManager.LoadSceneAsync(_currentSceneName);	
			}
		}

		public void LoadMainMenu()
		{
			Debug.Log("App: Loading MainMenu.");
			LoadScene(SceneNames.MainMenu);
		}



		public void PauseGame()
		{
			Time.timeScale = GameSpeeds.Paused;
		}

		public void HandleSpeedChange()
		{
			//Speed is progressively increased until it loops back to normal speed. If paused, go to normal speed
			switch ((int)Time.timeScale)
			{
			case GameSpeeds.Paused: 
				Time.timeScale = GameSpeeds.NormalSpeed; 
				break;
			case GameSpeeds.NormalSpeed: 
				Time.timeScale = GameSpeeds.DoubleSpeed; 
				break;
			case GameSpeeds.DoubleSpeed:
				Time.timeScale = GameSpeeds.FourTimesSpeed; 
				break;
			case GameSpeeds.FourTimesSpeed: 
				Time.timeScale = GameSpeeds.EightTimesSpeed;
				break;
			case GameSpeeds.EightTimesSpeed:
				Time.timeScale = GameSpeeds.NormalSpeed;
				break;
			default:
				Debug.LogError("Game speed reached unrecognised speed, resetting to normal");
				Time.timeScale = GameSpeeds.NormalSpeed;
				break;
			}
		}

		private void IncrementStarDate()
		{
			var previousStarDate = GameState.StarDate;
			GameState.StarDate = GameState.StarDate.AddHours(1);
			if (GameState.StarDate.Day != previousStarDate.Day)
				ProcessDay();
		}

		private void ProcessDay()
		{

			foreach (var colony in Model.Planets.Where(x => x.Colony != null).Select(x => x.Colony))
			{
				colony.ProcessColony();
			}

        
			GameState.Energon++;
			GameState.Detoxin++;
			GameState.Kremir++;
			GameState.Lepitium++;
			GameState.Raenium++;
			GameState.Texon++;
		}
        
	}
}
