using System.Collections;
using System.Collections.Generic;
using Assets.Code.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Behaviours
{
	public class ColonyListItemBehaviour : MonoBehaviour {

		public Colony Colony;

		void Update()
		{
			if (Colony != null)
			{
				if (!Colony.IsMiningColony)
				{
					var population = transform.Find("Population");
					population.GetComponent<Text>().text = Colony.Population.ToString("n0");	
					var taxRate = transform.Find("TaxRate");
					taxRate.GetComponent<Text>().text = (Colony.TaxRateInPercent / 100.0).ToString("p0");	
				}

			}

		}
	}
}

