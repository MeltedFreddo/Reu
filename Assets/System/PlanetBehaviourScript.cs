using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetBehaviourScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{		
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit && hitInfo.transform.gameObject == this.gameObject) 
			{
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);

				//SceneManager.UnloadSceneAsync("SystemView");
				SceneManager.LoadSceneAsync("SurfaceView");
				//var systemViewScene = SceneManager.GetSceneByName("SurfaceView");
				//SceneManager.SetActiveScene(systemViewScene);

			} else {
				
			}
		}
	}
}
