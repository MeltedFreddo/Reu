namespace Assets.Code.Data
{
	public class Building
	{
		public string BuildingType { get; set; }

        public decimal Cost { get; set; }

        public int PowerConsumption { get; set; }

		public bool IsActive { get; set; }

		public float X { get; set; }

		public float Y { get; set; }

		public int HeightInTiles { get; set; }

		public int WidthInTiles { get; set; }

	}

	public static class BuildingHelpers 
	{
		public static Building Clone(this Building building)
		{
			return new Building
			{
				BuildingType = building.BuildingType,
				Cost = building.Cost,
				PowerConsumption = building.PowerConsumption,
				IsActive = building.IsActive,
				X = building.X,
				Y = building.Y,
				HeightInTiles = building.HeightInTiles,
				WidthInTiles = building.WidthInTiles
			};
		}

		public static Building GetInstance(this Building building, bool isActive, float x, float y)
		{
			var result = building.Clone();
			result.IsActive = isActive;
			result.X = x;
			result.Y = y;
			return result;
		}
	}
}

