using System.Collections.Generic;
using System.Linq;
using Assets.Code.BaseClasses;
using Assets.Code.Data.Lists;
using Assets.Code.EventHandlers;
using UnityEngine;

namespace Assets.Code.Controllers
{
	public class SystemViewController : AppMonoBehaviour
	{
		private GameObject theStar;
		private ICollection<GameObject> planets;

		public GameObject StarPrefab;
		public GameObject HasColonyFlagPrefab;
		public List<GameObject> PlanetPrefabs;

		// Use this for initialization
		void Start()
		{
			var model = App.Instance.Model;

			//TODO load star and planets from game state
			theStar = Instantiate(StarPrefab);
			theStar.AddComponent<SphereCollider>();

			planets = new List<GameObject>();

			for (var i = 0; i < model.Planets.Count(); i++)
			{
				var thisPlanet = model.Planets.ElementAt(i);

				float radius = i + 1.5f;
				float angle = i * Mathf.PI * 2 / 5;
				Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

				var planetPrefab = PlanetPrefabs.Single(x => x.name == thisPlanet.SpriteName);
				var planetGameObject = Instantiate(planetPrefab, pos, theStar.transform.rotation);
				var collider = planetGameObject.AddComponent<SphereCollider>();
				var planetBehaviourScript = planetGameObject.GetComponent<PlanetBehaviour>();
				planetBehaviourScript.Planet = thisPlanet;

                planets.Add(planetGameObject);

				if (thisPlanet.Colony != null)
				{
					var icon = Instantiate(HasColonyFlagPrefab, pos, theStar.transform.rotation);
					icon.transform.SetParent(planetGameObject.transform);

					var thisPlanetScale = planetGameObject.transform.localScale.x;
					var iconCollider = icon.AddComponent<SphereCollider>();
					var iconShiftVector = new Vector3(0, (collider.radius + iconCollider.radius + 1) * thisPlanetScale, 0);
					iconShiftVector = theStar.transform.rotation * iconShiftVector;
					icon.transform.position += iconShiftVector;

				}
			}        
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetMouseButtonUp(0))
			{		
				RaycastHit hitInfo = new RaycastHit();
				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
				if (hit) // && hitInfo.transform.gameObject == gameObject)
				{
					Debug.Log("Hit " + hitInfo.transform.gameObject.name);

					var planet = hitInfo.transform.gameObject.GetComponent<PlanetBehaviour>().Planet;
					App.Instance.CurrentPlanet = planet;
					App.Instance.LoadScene(SceneNames.SurfaceView);

				}
			}

			//orbit planets around star
			foreach (var planet in planets)
			{
				var planetBehaviourScript = planet.GetComponent<PlanetBehaviour>();
				var planetPosition = planet.transform.position;
				var distanceFromSun = (theStar.transform.position - planetPosition).magnitude;            
				var orbitSpeed = 50 / distanceFromSun;
				planet.transform.RotateAround(theStar.transform.position, new Vector3(0, 1, 0), orbitSpeed * Time.deltaTime); // (1 is left) (-1 is right)
				planet.transform.rotation = StarPrefab.transform.rotation;

				//update scale
				var distanceFromZPlane = (new Vector3(planetPosition.x, planetPosition.y, 0) - planetPosition).z;           
				var planetScale = planetBehaviourScript.Planet.Size; //0.1
				var newScale = 0f;
				if (distanceFromZPlane != 0)
				{
					newScale = planetScale + ConvertDistanceToScale(5, distanceFromZPlane, planetScale);
				}
				planet.transform.localScale = new Vector3(newScale, newScale, newScale);

			}   
		}

		private float ConvertDistanceToScale(float maxDistance, float actualDistance, float planetScale)
		{
			var minDistance = maxDistance * -1;
			//the size of the planet scale can vary by half its scale so for a 0.1 scale sprite it will grow and shrink 0.05 either site
			var maxScale = planetScale / 2;
			var minScale = maxScale * -1;
			return (((actualDistance - minDistance) * (maxScale - minScale)) / (maxDistance - minDistance)) + minScale;
		}

	}
}
