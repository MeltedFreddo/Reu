using Assets.Code.Data.Lists;

namespace Assets.Code.Data
{
	public class Building
	{
		public string BuildingType { get; set; }

		public int PowerConsumption { get; set; }

		public bool IsActive { get; set; }

		public float X { get; set; }

		public float Y { get; set; }

		public int HeightInTiles { get; set; }

		public int WidthInTiles { get; set; }
	}
}

