using System.Collections.Generic;
using System.Linq;
using Assets.Code.BaseClasses;
using Assets.Code.Behaviours;
using Assets.Code.Data;
using Assets.Code.EventHandlers;
using Assets.Code.Data.Lists;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class SurfaceViewController : AppMonoBehaviour
    {
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


            if (currentColony != null)
            {
				var allBuildings = currentColony.Buildings.ToList();
                for (var i = 0; i < allBuildings.Count; i++)
                {
                    var thisBuilding = allBuildings.ElementAt(i);
                    var buildingPrefab = BuildingPrefabs.Single(x => x.name == thisBuilding.BuildingType);
					var pos = new Vector3(thisBuilding.X + (thisBuilding.WidthInTiles / 2f), thisBuilding.Y + (thisBuilding.HeightInTiles / 2f), 0);
                    var newBuildingGameObject = Instantiate(buildingPrefab, pos, Quaternion.identity);
					newBuildingGameObject.AddComponent<BoxCollider2D>();
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

        }

        public void AddBuildingButtonClick()
        {
            var building = new Building
            {
                BuildingType = BuildingType.Derrick,
                HeightInTiles = 2,
                WidthInTiles = 3,
            };
            AddBuilding(building);
        }

        public void AddBuildingButtonClick2()
        {
            var building = new Building
            {
                BuildingType = BuildingType.Spaceport,
                HeightInTiles = 4,
                WidthInTiles = 4,
            };
            AddBuilding(building);
        }

        public void AddBuildingButtonClick3()
        {
            var building = new Building
            {
                BuildingType = BuildingType.Residence,
                HeightInTiles = 2,
                WidthInTiles = 2,
            };
            AddBuilding(building);
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

            for (var i = 0; i < building.WidthInTiles; i++)
            {
                for (var j = 0; j < building.HeightInTiles; j++)
                {
                    var tilePosition = 
                        new Vector3(
                            buildingGameObject.transform.position.x + i * 0.25f - building.WidthInTiles * 0.25f,
                            buildingGameObject.transform.position.y + j * 0.25f - building.WidthInTiles * 0.25f,
                            buildingGameObject.transform.position.z
                            );

                    var tileOutline = Instantiate((GameObject)Resources.Load("Prefabs/Buildings/PlacementOutline"), tilePosition, Quaternion.identity);

                    tileOutline.transform.localScale =
                        new Vector3(
                            tileOutline.transform.localScale.x / 2,
                            tileOutline.transform.localScale.y / 2,
                            tileOutline.transform.localScale.z
                        );

                    tileOutline.tag = "PlacementOutline";
                    tileOutline.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    tileOutline.transform.parent = buildingGameObject.transform;
                }
            }
        }
    }
}
