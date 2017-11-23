using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Lists;

namespace Data
{
	public class Building {
		public BuildingType BuildingType { get; set; }
		public int PowerConsumption { get; set; }
		public bool IsActive { get; set; }
	}
}

