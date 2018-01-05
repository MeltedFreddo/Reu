using Assets.Code.BaseClasses;
using Assets.Code.Behaviours;
using Assets.Code.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Assets.Code.Controllers
{
	public class ColonyListController : AppMonoBehaviour
	{
		//prefabs
		public GameObject ColoniesHeading;
		public GameObject ColonyRow;
		public GameObject MiningStationsHeading;
		public GameObject MiningStationRow;

		public List<GameObject> PlanetPrefabs;

		// Use this for initialization
		void Start()
		{

			var colonyListContent = GameObject.Find("ColonyListContent");

			var planets = App.Instance.Model.Planets.Where(x => x.Colony != null);

			Debug.Log(colonyListContent.GetComponent<RectTransform>().sizeDelta);

			var contentPanelMinHeight = 515;
			var currentGridYPosition = new Vector3(0, -30, 0);

			if (planets.Any())
			{
				

				if (planets.Any(x => !x.Colony.IsMiningColony))
				{
					var coloniesHeading = Instantiate(ColoniesHeading);
					var rowTransform = coloniesHeading.GetComponent<RectTransform>();
					rowTransform.SetParent(colonyListContent.transform);
					rowTransform.localScale = new Vector3(1, 1, 1);
					rowTransform.localPosition = currentGridYPosition;
					rowTransform.offsetMax = new Vector2(0, rowTransform.offsetMax.y);

					currentGridYPosition = currentGridYPosition + new Vector3(0, -90, 0);
				}

				//normal colony grid
				foreach (var planet in planets.Where(x => !x.Colony.IsMiningColony))
				{
					var colony = planet.Colony;
					var colonyRow = Instantiate(ColonyRow);
					var rowTransform = colonyRow.GetComponent<RectTransform>();
					rowTransform.SetParent(colonyListContent.transform);
					rowTransform.localScale = new Vector3(1, 1, 1);
					rowTransform.localPosition = currentGridYPosition;
					rowTransform.offsetMax = new Vector2(0, rowTransform.offsetMax.y);

					var colonyListItemBehaviour = colonyRow.AddComponent<ColonyListItemBehaviour>();
					colonyListItemBehaviour.Colony = colony;

					SetPlanetNameAndImage(planet, rowTransform);

					currentGridYPosition = currentGridYPosition + new Vector3(0, -100, 0);


				}	

				if (planets.Any(x => x.Colony.IsMiningColony))
				{
					var miningStationsHeading = Instantiate(MiningStationsHeading);
					var rowTransform = miningStationsHeading.GetComponent<RectTransform>();
					rowTransform.SetParent(colonyListContent.transform);
					rowTransform.localScale = new Vector3(1, 1, 1);
					rowTransform.localPosition = currentGridYPosition;
					rowTransform.offsetMax = new Vector2(0, rowTransform.offsetMax.y);

					currentGridYPosition = currentGridYPosition + new Vector3(0, -90, 0);
				}

				//mining colony grid
				foreach (var planet in planets.Where(x => x.Colony.IsMiningColony))
				{
					var miningStationRow = Instantiate(MiningStationRow);
					var rowTransform = miningStationRow.GetComponent<RectTransform>();
					rowTransform.SetParent(colonyListContent.transform);
					rowTransform.localScale = new Vector3(1, 1, 1);
					rowTransform.localPosition = currentGridYPosition;
					rowTransform.offsetMax = new Vector2(0, rowTransform.offsetMax.y);

					SetPlanetNameAndImage(planet, rowTransform);

					currentGridYPosition = currentGridYPosition + new Vector3(0, -100, 0);
				}	
			}

			var contentMinRequiredHeight = Mathf.Abs(currentGridYPosition.y) - 30;
			Debug.Log(contentMinRequiredHeight);

			colonyListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Mathf.Max(contentPanelMinHeight, contentMinRequiredHeight));
		}

		private void SetPlanetNameAndImage(Planet planet, Transform rowTransform)
		{
			var sourcePlanetImage = PlanetPrefabs.Single(x => x.name == planet.SpriteName);
			var planetImage = rowTransform.Find("PlanetImage").GetComponent<Image>();
			planetImage.sprite = sourcePlanetImage.GetComponent<SpriteRenderer>().sprite;

			var planetName = rowTransform.Find("PlanetName");
			planetName.GetComponent<Text>().text = planet.PlanetName;
		}


		// Update is called once per frame
		void Update()
		{
		
		}
	}
}