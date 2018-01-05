using UnityEngine;

namespace Assets.Code.Data
{
    public class Planet
    {
		public string PlanetName { get; set; }
        public string SpriteName { get; set; }
        public float Size { get; set; }
        public Colony Colony { get; set; }

    }
}
