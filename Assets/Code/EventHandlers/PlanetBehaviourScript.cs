using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Code.Controllers;
using Assets.Code.Data.Lists;

namespace Assets.Code.EventHandlers
{
    public class PlanetBehaviourScript : MonoBehaviour {
	
        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
		
            if (Input.GetMouseButtonUp(0))
            {		
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit && hitInfo.transform.gameObject == this.gameObject) 
                {
                    Debug.Log("Hit " + hitInfo.transform.gameObject.name);

					App.Instance.LoadScene(SceneNames.SurfaceView);

                } else {
				
                }
            }
        }
    }
}
