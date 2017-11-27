using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Code.Controllers;
using Assets.Code.Data;
using Assets.Code.Data.Lists;

namespace Assets.Code.EventHandlers
{
    public class PlanetBehaviourScript : MonoBehaviour {
	
		public Planet Planet;

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
		
            if (Input.GetMouseButtonUp(0))
            {		
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit && hitInfo.transform.gameObject == gameObject) 
                {
                    Debug.Log("Hit " + gameObject.name);

					App.Instance.CurrentColony = Planet.Colony;
					App.Instance.LoadScene(SceneNames.SurfaceView);

                } else {
				
                }
            }
        }
    }
}
