using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SystemViewController : MonoBehaviour
{

    public GameObject theStar;
    public ICollection<Data.Planet> planets;

    public GameObject StarPrefab;
    public List<GameObject> PlanetPrefabs;

    // Use this for initialization
    void Start()
    {
        var model = new Data.Model();

        //TODO load star and planets from game state
        theStar = Instantiate(StarPrefab);
        
        planets = new List<Data.Planet>();

        for (var i = 0; i < model.Planets.Count(); i++)
        {
            var thisPlanet = model.Planets.ElementAt(i);

            float radius = i + 1.5f;
            float angle = i * Mathf.PI * 2 / 5;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            var thing = PlanetPrefabs.ElementAt(0);

            var planetPrefab = PlanetPrefabs.Single(x => x.name == thisPlanet.SpriteName);
            thisPlanet.GameObject = Instantiate(planetPrefab, pos, planetPrefab.transform.rotation);
                        
            planets.Add(thisPlanet);
        }        
    }

    // Update is called once per frame
    void Update()
    {                
        //orbit planets around star
        foreach (var planet in planets)
        {
            var planetPosition = planet.GameObject.transform.position;
            var distanceFromSun = (theStar.transform.position - planetPosition).magnitude;            
            var orbitSpeed = 50 / distanceFromSun;
            planet.GameObject.transform.RotateAround(theStar.transform.position, new Vector3(0, 1, 0), orbitSpeed * Time.deltaTime); // (1 is left) (-1 is right)
            planet.GameObject.transform.rotation = StarPrefab.transform.rotation;

            //update scale
            var distanceFromZPlane = (new Vector3(planetPosition.x, planetPosition.y, 0) - planetPosition).z;           
            var planetScale = planet.Size; //0.1
            var newScale = 0f;
            if (distanceFromZPlane != 0)
            {
                newScale = planetScale + ConvertDistanceToScale(5, distanceFromZPlane);
            }
            planet.GameObject.transform.localScale = new Vector3(newScale, newScale, newScale);

        }   
    }

    private float ConvertDistanceToScale(float maxDistance, float actualDistance)
    {
        return (((actualDistance - maxDistance * -1f) * (0.05f - -0.05f)) / (maxDistance - maxDistance * -1f)) + -0.05f;
    }

}
