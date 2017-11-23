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
            Planets = new List<Planet>
                    {
				new Planet {SpriteName = PlanetSprites.EarthPlanet, Size = 0.1f, Colony = new Colony()},
                        new Planet {SpriteName = PlanetSprites.BluePlanet, Size = 0.12f},
                        new Planet {SpriteName = PlanetSprites.RedPlanet, Size = 0.07f},
                        new Planet {SpriteName = PlanetSprites.WaterPlanet, Size = 0.09f},
                        new Planet {SpriteName = PlanetSprites.DarkPlanet, Size = 0.18f},
                        new Planet {SpriteName = PlanetSprites.PurplePlanet, Size = 0.12f},
                    };
                
            _gameState = new GameState();
        }

        public void Run()
        {
            
        }
    }
}
