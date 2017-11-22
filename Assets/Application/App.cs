using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Lists;
using UnityEngine.SceneManagement;

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

		Time.timeScale = 1;

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
		Debug.Log("App: Loading Scene, " + sceneName);

		CurrentSceneName = sceneName;

		SceneManager.LoadSceneAsync(CurrentSceneName);
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

    private void IncrementStarDate()
    {
        var previousStarDate = GameState.StarDate;
        GameState.StarDate = GameState.StarDate.AddHours(1);
        if (GameState.StarDate.Day != previousStarDate.Day)
            ProcessDay();
    }

	private void ProcessDay(){
        GameState.Money++;
        GameState.Energon++;
        GameState.Detoxin++;
        GameState.Kremir++;
        GameState.Lepitium++;
        GameState.Raenium++;
        GameState.Texon++;
    }
}
