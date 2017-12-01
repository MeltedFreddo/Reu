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
                    var newBuildingGameObject = Instantiate(buildingPrefab, pos, Quaternion.identity);
					var collider2D = newBuildingGameObject.AddComponent<BoxCollider2D>();

					highlightAroundCollider(collider2D, Color.yellow, Color.red, 0.1f);
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

		void highlightAroundCollider(Component cpType, Color beginColor, Color endColor, float hightlightSize = 0.3f)
		{
			//1. Create new Line Renderer
			LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
			if (lineRenderer == null)
			{
				lineRenderer = cpType.gameObject.AddComponent<LineRenderer>();
			}

			//2. Assign Material to the new Line Renderer
			lineRenderer.material = new Material(Shader.Find("Particles/Additive"));

			float zPos = 0.1f;//Since this is 2D. Make sure it is in the front

			if (cpType is PolygonCollider2D)
			{
				//3. Get the points from the PolygonCollider2D
				Vector2[] pColiderPos = (cpType as PolygonCollider2D).points;

				//Set color and width
				lineRenderer.SetColors(beginColor, endColor);
				lineRenderer.SetWidth(hightlightSize, hightlightSize);

				//4. Convert local to world points
				for (int i = 0; i < pColiderPos.Length; i++)
				{
					pColiderPos[i] = cpType.transform.TransformPoint(pColiderPos[i]);
				}

				//5. Set the SetVertexCount of the LineRenderer to the Length of the points
				lineRenderer.SetVertexCount(pColiderPos.Length + 1);
				for (int i = 0; i < pColiderPos.Length; i++)
				{
					//6. Draw the  line
					Vector3 finalLine = pColiderPos[i];
					finalLine.z = zPos;
					lineRenderer.SetPosition(i, finalLine);

					//7. Check if this is the last loop. Now Close the Line drawn
					if (i == (pColiderPos.Length - 1))
					{
						finalLine = pColiderPos[0];
						finalLine.z = zPos;
						lineRenderer.SetPosition(pColiderPos.Length, finalLine);
					}
				}
			}

			//Not Implemented. You can do this yourself
			else if (cpType is BoxCollider2D)
			{
				var boxColliderPoints = GetBoxCorners((BoxCollider2D)cpType);

				//Set color and width
				lineRenderer.SetColors(beginColor, endColor);
				lineRenderer.SetWidth(hightlightSize, hightlightSize);

				//4. Convert local to world points
				/*
				for (int i = 0; i < boxColliderPoints.Count; i++)
				{
					boxColliderPoints.ElementAt(i) = cpType.transform.TransformPoint(boxColliderPoints.ElementAt(i));
				}
				*/

				//5. Set the SetVertexCount of the LineRenderer to the Length of the points
				lineRenderer.SetVertexCount(boxColliderPoints.Count() + 1);
				for (int i = 0; i < boxColliderPoints.Count(); i++)
				{
					//6. Draw the  line
					Vector3 finalLine = boxColliderPoints.ElementAt(i);
					finalLine.z = zPos;
					lineRenderer.SetPosition(i, finalLine);

					//7. Check if this is the last loop. Now Close the Line drawn
					if (i == (boxColliderPoints.Count() - 1))
					{
						finalLine = boxColliderPoints.ElementAt(0);
						finalLine.z = zPos;
						lineRenderer.SetPosition(boxColliderPoints.Count(), finalLine);
					}
				}
			}
		}

		private IEnumerable<Vector3> GetBoxCorners(BoxCollider2D boxCollider2D) {
			var results = new List<Vector3>();

			Transform bcTransform = boxCollider2D.transform;

			// The collider's centre point in the world
			Vector3 worldPosition = bcTransform.TransformPoint(0, 0, 0);

			// The collider's local width and height, accounting for scale, divided by 2
			Vector2 size = new Vector2(boxCollider2D.size.x * bcTransform.localScale.x * 0.5f, boxCollider2D.size.y * bcTransform.localScale.y * 0.5f);

			// STEP 1: FIND LOCAL, UN-ROTATED CORNERS
			// Find the 4 corners of the BoxCollider2D in LOCAL space, if the BoxCollider2D had never been rotated
			Vector3 corner1 = new Vector2(-size.x, -size.y);
			Vector3 corner2 = new Vector2(-size.x, size.y);
			Vector3 corner3 = new Vector2(size.x, size.y);
			Vector3 corner4 = new Vector2(size.x, -size.y);

			// STEP 2: ROTATE CORNERS
			// Rotate those 4 corners around the centre of the collider to match its transform.rotation
			corner1 = RotatePointAroundPivot(corner1, Vector3.zero, bcTransform.eulerAngles);
			corner2 = RotatePointAroundPivot(corner2, Vector3.zero, bcTransform.eulerAngles);
			corner3 = RotatePointAroundPivot(corner3, Vector3.zero, bcTransform.eulerAngles);
			corner4 = RotatePointAroundPivot(corner4, Vector3.zero, bcTransform.eulerAngles);

			// STEP 3: FIND WORLD POSITION OF CORNERS
			// Add the 4 rotated corners above to our centre position in WORLD space - and we're done!
			results.Add(worldPosition + corner1);
			results.Add(worldPosition + corner2);
			results.Add(worldPosition + corner3);
			results.Add(worldPosition + corner4);

			return results;
		}

		// Helper method courtesy of @aldonaletto
		// http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
		Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
			Vector3 dir = point - pivot; // get point direction relative to pivot
			dir = Quaternion.Euler(angles) * dir; // rotate it
			point = dir + pivot; // calculate rotated point
			return point; // return it
		}
    }
}
