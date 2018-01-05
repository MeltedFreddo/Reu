using System.Collections.Generic;
using Assets.Code.Data.GameContent;
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
				new Planet {
						PlanetName = "Blahrada",
						SpriteName = PlanetSprites.EarthPlanet, Size = 0.1f, Colony = new Colony {
						Population = 10000,
						Buildings = new List<Building>
						{
							Buildings.Residence.GetInstance(true, 0, 0),
							Buildings.PowerPlant.GetInstance(true, 1, 1),
							Buildings.Derrick.GetInstance(true, -1, -1),
							Buildings.Administration.GetInstance(true, 1, -1),
							Buildings.Derrick.GetInstance(true, -1, -2),
							Buildings.Farm.GetInstance(true, -1, 0),
							Buildings.Hospital.GetInstance(true, 0.5f, -3),
							Buildings.Spaceport.GetInstance(true, -4, 1),
							Buildings.Observatory.GetInstance(true, -1, -3),
							Buildings.Mine.GetInstance(true, 6, 0),
							Buildings.Mine.GetInstance(true, 6, 1.5f),
							Buildings.Mine.GetInstance(true, 6, 3),
                        }
					}
				},
				new Planet { PlanetName = "Virage", SpriteName = PlanetSprites.BluePlanet, Size = 0.12f },
				new Planet { PlanetName = "John", SpriteName = PlanetSprites.RedPlanet, Size = 0.07f },
				new Planet { PlanetName = "Cheesetown", SpriteName = PlanetSprites.WaterPlanet, Size = 0.09f },
				new Planet { PlanetName = "The drak plant", SpriteName = PlanetSprites.DarkPlanet, Size = 0.18f, Colony = new Colony {
						Buildings = new List<Building> {
							Buildings.MiningStation.GetInstance(true, 0, 0)
						}
					}
				},
				new Planet { PlanetName = "A solid plan(et)", SpriteName = PlanetSprites.PurplePlanet, Size = 0.12f },
			};
                
			_gameState = new GameState();
		}

		public void Run()
		{
            
		}
	}
}
