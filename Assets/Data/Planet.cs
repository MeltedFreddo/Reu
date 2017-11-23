using UnityEngine;

namespace Data
{
    public class Planet : UnityEngine.Component
    {
        public string SpriteName { get; set; }
        public float Size { get; set; }
        public Colony Colony { get; set; }

        public GameObject GameObject { get; set; }
    }
}
