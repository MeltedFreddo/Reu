using System.Linq;
using Assets.Code.Data.Lists;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Controllers
{
    public class App : MonoBehaviour {
	
        static App Singleton = null;
        public static App Instance { get { return Singleton; } }

        private string CurrentSceneName;
        public Data.GameState GameState;
        public Data.Model Model;

        void Awake(){
            if (Singleton != null) {	
                Debug.LogError ("App Singleton pattern violated");
            }

            Singleton = this;

            GameState = new Data.GameState();
            Model = new Data.Model ();



            // Are we in the Application Scene?
            if ( SceneManager.GetActiveScene().name == "Application" )
            {
                // Make sure this object persists between scene loads.
                DontDestroyOnLoad(gameObject);

                LoadMainMenu();
            }
        }

        // Use this for initialization
        void Start () {	
            InvokeRepeating ("IncrementStarDate", 0, 1);
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        public void LoadScene( string sceneName )
        {
            if (SceneManager.GetActiveScene ().name != sceneName) {
                Debug.Log ("App: Loading Scene, " + sceneName);

                CurrentSceneName = sceneName;

                SceneManager.LoadSceneAsync (CurrentSceneName);	
            }
        }

        public void LoadMainMenu()
        {
            Debug.Log("App: Loading MainMenu.");
            LoadScene( SceneNames.MainMenu );
        }

        public void Quit()
        {
            Debug.Log("App: Terminating.");

            Application.Quit();
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
                    Time.timeScale = GameSpeeds.QuadrupleSpeed; 
                    break;
                case GameSpeeds.QuadrupleSpeed: 
                    Time.timeScale = GameSpeeds.NormalSpeed; 
                    break;
                default:
                    Debug.LogError ("Game speed reached unrecognised speed, resetting to normal");
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

        private void ProcessDay(){

            foreach (var colony in Model.Planets.Where(x => x.Colony != null).Select(x => x.Colony)) 
            {
                colony.ProcessColony ();
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
