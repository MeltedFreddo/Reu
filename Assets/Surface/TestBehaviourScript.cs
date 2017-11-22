using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{		
			var hitInfo = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

			if (hitInfo.transform != null && hitInfo.transform.gameObject == this.gameObject) 
			{
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);

				//SceneManager.UnloadSceneAsync("SystemView");
				var systemViewScene = SceneManager.GetSceneByName("SystemView");
				SceneManager.SetActiveScene(systemViewScene);

			} else {

			}
		}
	}
}
