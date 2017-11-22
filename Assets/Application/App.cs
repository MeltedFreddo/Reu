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
		InvokeRepeating ("ProcessDay", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel( string level )
	{
		Debug.Log("App: Loading Level, " + level);

		CurrentSceneName = level;

		SceneManager.LoadSceneAsync(CurrentSceneName);
	}

	public void LoadMainMenu()
	{
		Debug.Log("App: Loading MainMenu.");
		LoadLevel( SceneNames.MainMenu );
	}


	private void ProcessDay(){
		var thing = "";
	}
}
