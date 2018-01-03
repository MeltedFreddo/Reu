using Assets.Code.BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

		// Use this for initialization
		void Start()
		{

			var colonyListContent = GameObject.Find("ColonyListContent");

			var colonies = App.Instance.Model.Planets.Where(x => x.Colony != null).Select(x => x.Colony);

			Debug.Log("starting colony list");

			if (colonies.Any())
			{
				var currentGridYPosition = new Vector3(0, -30, 0);

				if (colonies.Any(x => !x.IsMiningColony))
				{
					Debug.Log("printing colony header");
					var coloniesHeading = Instantiate(ColoniesHeading);
					coloniesHeading.transform.SetParent(colonyListContent.transform, false);
					//coloniesHeading.transform.localScale = new Vector3(1, 1, 1);
					coloniesHeading.transform.position = currentGridYPosition;

					currentGridYPosition = currentGridYPosition + new Vector3(0, -90, 0);
				}

				//normal colony grid
				foreach (var colony in colonies.Where(x => !x.IsMiningColony))
				{
					Debug.Log("printing a colony");
					var colonyRow = Instantiate(ColonyRow);
					colonyRow.transform.SetParent(colonyListContent.transform, false);
					//colonyRow.transform.localScale = new Vector3(1, 1, 1);
					//colonyRow.transform.position = currentGridYPosition;

					currentGridYPosition = currentGridYPosition + new Vector3(0, -100, 0);
				}	

				if (colonies.Any(x => x.IsMiningColony))
				{
					Debug.Log("printing mining station header");
					var miningStationsHeading = Instantiate(MiningStationsHeading);
					miningStationsHeading.transform.SetParent(colonyListContent.transform, false);
					//miningStationsHeading.transform.localScale = new Vector3(1, 1, 1);
					miningStationsHeading.transform.position = currentGridYPosition;

					currentGridYPosition = currentGridYPosition + new Vector3(0, -90, 0);
				}

				//mining colony grid
				foreach (var colony in colonies.Where(x => x.IsMiningColony))
				{
					Debug.Log("printing a mining station");
					var miningStationRow = Instantiate(MiningStationRow);
					miningStationRow.transform.SetParent(colonyListContent.transform, false);
					//miningStationRow.transform.localScale = new Vector3(1, 1, 1);
					miningStationRow.transform.position = currentGridYPosition;

					currentGridYPosition = currentGridYPosition + new Vector3(0, -100, 0);
				}	
			}
		}

		// Update is called once per frame
		void Update()
		{
		
		}
	}
}