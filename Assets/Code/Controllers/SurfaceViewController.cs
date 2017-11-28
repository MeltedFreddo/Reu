using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.Code.BaseClasses;
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
			var currentColony = currentPlanet.Colony;

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
                    Instantiate(buildingPrefab, pos, Quaternion.identity);
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
    }
}
