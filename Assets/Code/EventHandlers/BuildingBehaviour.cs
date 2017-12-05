using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonUp(0))
		{
			//Converting Mouse Pos to 2D (vector2) World Pos
			Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			var hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

			if (hit)
			{
				Debug.Log(hit.transform.name);
				ToggleBuildingSelection(hit.transform.gameObject);
			}
		}
	}

	void ToggleBuildingSelection(GameObject building)
	{		
		Instantiate(Resources.Load("Prefabs/Buildings/SelectedOutline"), building.transform.position, Quaternion.identity);
	}
}
