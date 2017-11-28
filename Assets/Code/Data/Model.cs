using System.Collections.Generic;
using Assets.Code.Data.Lists;

namespace Assets.Code.Data
{
	public class Model
	{
		public IEnumerable<Planet> Planets { get; set; }

		private GameState _gameState { get; set; }

		public Model()
		{
			Planets = new List<Planet> {
				new Planet {SpriteName = PlanetSprites.EarthPlanet, Size = 0.1f, Colony = new Colony {
						Buildings = new List<Building> {
							new Building { BuildingType = BuildingType.Residence, IsActive = true, PowerConsumption = 1, X = 0, Y = 0, HeightInTiles = 1f, WidthInTiles = 1f },
						    new Building { BuildingType = BuildingType.PowerPlant, IsActive = true, PowerConsumption = 1, X = 1, Y = 1, HeightInTiles = 2f, WidthInTiles = 2f },
							new Building { BuildingType = BuildingType.Derrick, IsActive = true, PowerConsumption = 1, X = -1, Y = -1, HeightInTiles = 1f, WidthInTiles = 1.5f },
                            new Building { BuildingType = BuildingType.Administration, IsActive = true, PowerConsumption = 1, X = 1, Y = -1, HeightInTiles = 2f, WidthInTiles = 2f },
						    new Building { BuildingType = BuildingType.Derrick, IsActive = true, PowerConsumption = 1, X = -1, Y = -2, HeightInTiles = 1f, WidthInTiles = 1.5f },
						    new Building { BuildingType = BuildingType.Farm, IsActive = true, PowerConsumption = 1, X = -1, Y = 0, HeightInTiles = 1f, WidthInTiles = 1f },
						    new Building { BuildingType = BuildingType.Hospital, IsActive = true, PowerConsumption = 1, X = 0.5f, Y = -3, HeightInTiles = 2f, WidthInTiles = 2f },
						    new Building { BuildingType = BuildingType.Spaceport, IsActive = true, PowerConsumption = 1, X = -4, Y = 1, HeightInTiles = 2f, WidthInTiles = 2f },
						    new Building { BuildingType = BuildingType.Observatory, IsActive = true, PowerConsumption = 1, X = -1, Y = 3, HeightInTiles = 1.5f, WidthInTiles = 1.5f },
                        }
					}
				},
				new Planet { SpriteName = PlanetSprites.BluePlanet, Size = 0.12f },
				new Planet { SpriteName = PlanetSprites.RedPlanet, Size = 0.07f },
				new Planet { SpriteName = PlanetSprites.WaterPlanet, Size = 0.09f },
				new Planet {SpriteName = PlanetSprites.DarkPlanet, Size = 0.18f, Colony = new Colony {
						Buildings = new List<Building> {
							new Building { BuildingType = BuildingType.Residence, IsActive = true, PowerConsumption = 1, X = 0, Y = 0 },

						}
					}
				},
				new Planet { SpriteName = PlanetSprites.PurplePlanet, Size = 0.12f },
			};
                
			_gameState = new GameState();
		}

		public void Run()
		{
            
		}
	}
}
