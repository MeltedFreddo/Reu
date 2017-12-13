using System.Collections.Generic;
using Assets.Code.Data.Lists;

namespace Assets.Code.Data.GameContent
{
    public static class Buildings
    {
        public static Building Administration
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.Administration,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 4,
                    WidthInTiles = 4
                };
            }
        }
        public static Building Derrick
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.Derrick,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 2,
                    WidthInTiles = 3
                };
            }
        }
        public static Building Farm
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.Farm,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 2,
                    WidthInTiles = 2
                };
            }
        }
        public static Building Hospital
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.Hospital,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 4,
                    WidthInTiles = 4
                };
            }
        }
        public static Building Mine
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.Mine,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 3,
                    WidthInTiles = 3
                };
            }
        }
        public static Building MiningStation
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.MiningStation,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 4,
                    WidthInTiles = 4
                };
            }
        }
        public static Building Observatory
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.Observatory,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 3,
                    WidthInTiles = 3
                };
            }
        }
        public static Building Park
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.Park,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 2,
                    WidthInTiles = 2
                };
            }
        }
        public static Building PowerPlant
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.PowerPlant,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 4,
                    WidthInTiles = 4
                };
            }
        }
        public static Building Residence
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.Residence,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 2,
                    WidthInTiles = 2
                };
            }
        }
        public static Building Spaceport
        {
            get
            {
                return new Building
                {
                    BuildingType = BuildingType.Spaceport,
                    Cost = 10,
                    IsActive = true,
                    PowerConsumption = 1,
                    HeightInTiles = 4,
                    WidthInTiles = 4
                };
            }
        }

        public static IEnumerable<Building> GetAllBuildings()
        {
            return new List<Building>
            {
                Administration,
                Derrick,
                Farm,
                Hospital,
                Mine,
                MiningStation,
                Observatory,
                Park,
                PowerPlant,
                Residence,
                Spaceport
            };
        }
    }
}
