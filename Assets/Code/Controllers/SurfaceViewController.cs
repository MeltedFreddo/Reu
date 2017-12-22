using System.Collections.Generic;
using System.Linq;
using Assets.Code.BaseClasses;
using Assets.Code.Behaviours;
using Assets.Code.Data;
using Assets.Code.EventHandlers;
using Assets.Code.Data.GameContent;
using Assets.Code.Data.Lists;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Controllers
{
    public class SurfaceViewController : AppMonoBehaviour
    {
		public GameObject AddBuildingButtonPrefab;
		public List<GameObject> SurfaceTilePrefabs;
		public List<GameObject> BuildingPrefabs;

        // Use this for initialization
        void Start()
        {
            var currentPlanet = App.Instance.CurrentPlanet;
			var currentColony = currentPlanet != null ? currentPlanet.Colony : null;

			//TODO tile choice and planet size
			var surfaceTile = SurfaceTilePrefabs.Single(x => x.name == "Ash");
			var surfaceTilePos = new Vector3(0, 0, 0);
			var surfaceTileGameObject = Instantiate(surfaceTile, surfaceTilePos, Quaternion.identity);
			var surfaceTileSpriteRenderer = surfaceTileGameObject.GetComponent<SpriteRenderer>();
			surfaceTileSpriteRenderer.size = new Vector2(50, 50);

			// Create add building UI buttons
			var ui = GameObject.Find("SurfaceMenuBar");
			var buttonPosition = new Vector3(0, 190, 0);
			foreach (var building in Buildings.GetAllBuildings())
			{
				var button = Instantiate(AddBuildingButtonPrefab);
				button.transform.parent = ui.transform;
				button.transform.localScale = new Vector3(1, 1, 1);
				button.transform.localPosition = buttonPosition;
				var buildingBehaviour = button.AddComponent<BuildingBehaviour>();
				buildingBehaviour.Building = building;
				var buttonText = button.GetComponentInChildren<Text>();
				buttonText.text = building.BuildingType.ToString();
				var buttonScript = button.GetComponent<Button>();
				buttonScript.onClick.AddListener(() => AddBuildingButtonClick(button));

				buttonPosition = buttonPosition + new Vector3(0, -40, 0);
			}


            if (currentColony != null)
            {
				var allBuildings = currentColony.Buildings.ToList();
                for (var i = 0; i < allBuildings.Count; i++)
                {
                    var thisBuilding = allBuildings.ElementAt(i);
                    var buildingPrefab = BuildingPrefabs.Single(x => x.name == thisBuilding.BuildingType);
					var pos = new Vector3(thisBuilding.X + (thisBuilding.WidthInTiles / 4f), thisBuilding.Y - (thisBuilding.HeightInTiles / 4f), 0);
                    var newBuildingGameObject = Instantiate(buildingPrefab, pos, Quaternion.identity);
                    var buildingBehaviour = newBuildingGameObject.GetComponent<BuildingBehaviour>();
                    buildingBehaviour.Building = thisBuilding;
                }
					
            }
            else
            {
                
            }
                
        }

        // Update is called once per frame
        void Update()
        {
			if (Input.GetMouseButtonUp(0))
			{
				//Converting Mouse Pos to 2D (vector2) World Pos
				Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
				var hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

				if (hit)
				{
					Debug.Log("Toggling building selection of " + hit.transform.name);
					ToggleBuildingSelection(hit.transform.gameObject);
				}
				else
				{
					UnselectAllBuildings();
				}
			}
        }


		void UnselectAllBuildings()
		{
			var selectedOutlines = GameObject.FindGameObjectsWithTag("SelectedOutline");
			foreach (var outline in selectedOutlines)
			{
				Destroy(outline);
			}
		}

		void ToggleBuildingSelection(GameObject buildingGameObject)
		{
			var selectedOutline = buildingGameObject.transform.Find("SelectedOutline");
			if (selectedOutline != null)
			{
				Destroy(selectedOutline.gameObject);
			}
			else
			{
				UnselectAllBuildings();

				var selectedOutlineGameObject = Instantiate((GameObject)Resources.Load("Prefabs/Buildings/SelectedOutline"), buildingGameObject.transform.position, Quaternion.identity);
				selectedOutlineGameObject.name = "SelectedOutline";
				selectedOutlineGameObject.tag = "SelectedOutline";
				selectedOutlineGameObject.transform.parent = buildingGameObject.transform;
				var spriteRenderer = selectedOutlineGameObject.GetComponent<SpriteRenderer>();
				spriteRenderer.sortingOrder = 5;
				var building = buildingGameObject.GetComponent<BuildingBehaviour>().Building;

				selectedOutlineGameObject.transform.localScale =
					new Vector3(
						selectedOutlineGameObject.transform.localScale.x * building.WidthInTiles / 2,
						selectedOutlineGameObject.transform.localScale.y * building.HeightInTiles / 2,
						selectedOutlineGameObject.transform.localScale.z
					);
			}
		}

		public void AddBuildingButtonClick(GameObject sender)
		{
			var building = sender.GetComponent<BuildingBehaviour>().Building;
			AddBuilding(building.Clone());
		}

        private void AddBuilding(Building building)
        {
            var mouselocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var pos = new Vector3(mouselocation.x, mouselocation.y, 0);
            var buildingGameObject = Instantiate(BuildingPrefabs.Single(x => x.name == building.BuildingType), pos, Quaternion.identity);
            buildingGameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            var buildingBehaviour = buildingGameObject.GetComponent<BuildingBehaviour>();
            buildingBehaviour.Building = building;
            buildingGameObject.AddComponent<PlaceBuildingBehaviour>();

			var tileOutline = Instantiate((GameObject)Resources.Load("Prefabs/Buildings/PlacementOutline"), pos, Quaternion.identity);

			tileOutline.transform.localScale =
				new Vector3(
					tileOutline.transform.localScale.x / 2,
					tileOutline.transform.localScale.y / 2,
					tileOutline.transform.localScale.z
				);

			tileOutline.tag = "PlacementOutline";
			var spriteRenderer = tileOutline.GetComponent<SpriteRenderer>();
			spriteRenderer.sortingOrder = 3;
			spriteRenderer.size = new Vector2(building.WidthInTiles, building.HeightInTiles);
			tileOutline.transform.parent = buildingGameObject.transform;

        }
    }
}
