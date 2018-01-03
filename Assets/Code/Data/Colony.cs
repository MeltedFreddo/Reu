using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Controllers;
using Assets.Code.Data.Lists;

namespace Assets.Code.Data
{
	public class Colony
	{

		public ICollection<Building> Buildings { get; set; }

		public bool IsMiningColony
		{
			get
			{
				return Population == 0;
			}
		}

		public long Population { get; set; }

		public long HappinessInPercent { get; set; }

		public long TaxRateInPercent { get; set; }

		//TODO these settings could maybe go elsewhere
		private const decimal _avgSalaryPerYear = 35000;
		//annual wage per person 35k
		private const int _residenceCapacity = 100000;
		//100k per arcology thing
		private const int _maxPopulationGrowthRateInPercent = 2;
		//how fast the pop grows at 100% happiness

		public Colony()
		{
			Buildings = new List<Building>();

			HappinessInPercent = 80;
			TaxRateInPercent = 5;
		}

		public void OnBuildingAdded()
		{
			//TODO calculate energy use after new building add and turn off/on buildings to match available demand
		}

		//TODO invoke this every week
		public void ProcessColony()
		{
			if (Population > 0)
			{
				ProcessTaxation();
				ProcessHappiness();
				ProcessPopulationChange();
			}				
		}

		private void ProcessTaxation()
		{			
			decimal taxGenerated = Population * (_avgSalaryPerYear / 365) * ((decimal)TaxRateInPercent / 100);
			App.Instance.GameState.Money += taxGenerated;
		}

		private void ProcessHappiness()
		{
			//TODO logic for happiness
			var overcrowdingPenalty = 0;
			var taxationPenalty = 0;
			var recreationBonus = Buildings.Count(x => x.BuildingType == BuildingType.Park && x.IsActive);

			var happinessChange = 0;

			HappinessInPercent += happinessChange;
		}

		private void ProcessPopulationChange()
		{
			var populationIncreaseRate = ConvertHappinessToGrowth() / 100;

			//TODO health?

			//cap under the amount of free space
			var populationIncreaseAmount = Math.Min(CalculateFreeSpace(), Population * populationIncreaseRate); 

			Population += (long)populationIncreaseAmount;
		}

		private long CalculateFreeSpace()
		{
			var totalColonyCapacity = Buildings.Count(x => x.BuildingType == BuildingType.Residence && x.IsActive) * _residenceCapacity;
			return totalColonyCapacity - Population;
		}

		private float ConvertHappinessToGrowth()
		{
			var minGrowth = _maxPopulationGrowthRateInPercent * -1;
			var maxHappinessInPercent = 100;
			var minHappinessInPercent = 0;
		
			return (((HappinessInPercent - minHappinessInPercent) * (_maxPopulationGrowthRateInPercent - minGrowth)) / (maxHappinessInPercent - minHappinessInPercent)) + minGrowth;
		}
	}
}

