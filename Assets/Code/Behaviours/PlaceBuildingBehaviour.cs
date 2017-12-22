using Assets.Code.Data;
using Assets.Code.EventHandlers;
using Assets.Code.Controllers;
using UnityEngine;
using System.Linq;

namespace Assets.Code.Behaviours
{
	public class PlaceBuildingBehaviour : MonoBehaviour
	{
		
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			var mouselocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var nearestGridLocation = SnapVectorXandY(new Vector3(mouselocation.x, mouselocation.y, 0), gameObject.GetComponent<BuildingBehaviour>().Building);
			transform.position = nearestGridLocation;
            
			//var thing = Mathf.Round()

			if (Input.GetMouseButtonUp(0))
			{
				//ensure we can place building
				var boxCollider = transform.GetComponent<BoxCollider2D>();

				var hits = 
					Physics2D.BoxCastAll(nearestGridLocation, boxCollider.size, 0, Vector2.zero)
						.Where(x => x.transform != transform);
				
				if (!hits.Any())
				{
					//building is placed, add it to the colony and deregister this script
					Destroy(transform.GetComponent<PlaceBuildingBehaviour>());
					Destroy(GameObject.FindGameObjectsWithTag("PlacementOutline").First());

					var building = transform.GetComponent<BuildingBehaviour>().Building;
					var roundedMouselocation = RoundVectorToNearestWorldGridPoint(mouselocation);
					building.X = roundedMouselocation.x;
					building.Y = roundedMouselocation.y;
					App.Instance.CurrentPlanet.Colony.Buildings.Add(building);
					App.Instance.GameState.Money -= building.Cost;
					//Debug.Log(App.Instance.CurrentPlanet.Colony.Buildings.Count);
				}
                
			}
			if (Input.GetMouseButtonUp(1))
			{
				//building placement cancelled, destroy this object
				Destroy(gameObject);
			}
		}

		private Vector3 RoundVectorToNearestWorldGridPoint(Vector3 vectorToRound)
		{
			return new Vector3(
				Mathf.Floor(vectorToRound.x * 2) / 2,
				Mathf.Ceil(vectorToRound.y * 2) / 2,
				vectorToRound.z
			);
		}

		public Vector3 SnapVectorXandY(Vector3 vectorToSnap, Building building)
		{
			var roundedVector =
				RoundVectorToNearestWorldGridPoint(vectorToSnap);
			
			var offsetRoundedVector =
				new Vector3(
					roundedVector.x + 0.25f * building.WidthInTiles,
					(roundedVector.y - 0.25f * building.HeightInTiles),
					roundedVector.z
				);
			return offsetRoundedVector;
		}

	}
}
