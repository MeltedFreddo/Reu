using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Code.BaseClasses;

public class SystemViewController : AppMonoBehaviour
{
	private GameObject theStar;
    private ICollection<Data.Planet> planets;

    public GameObject StarPrefab;
    public List<GameObject> PlanetPrefabs;

    // Use this for initialization
    void Start()
    {
		var model = App.Instance.Model;

        //TODO load star and planets from game state
        theStar = Instantiate(StarPrefab);
		theStar.AddComponent<SphereCollider> ();

        planets = new List<Data.Planet>();

        for (var i = 0; i < model.Planets.Count(); i++)
        {
            var thisPlanet = model.Planets.ElementAt(i);

            float radius = i + 1.5f;
            float angle = i * Mathf.PI * 2 / 5;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            var planetPrefab = PlanetPrefabs.Single(x => x.name == thisPlanet.SpriteName);
            thisPlanet.GameObject = Instantiate(planetPrefab, pos, planetPrefab.transform.rotation);
			thisPlanet.GameObject.AddComponent<SphereCollider> ();

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
                newScale = planetScale + ConvertDistanceToScale(5, distanceFromZPlane, planetScale);
            }
            planet.GameObject.transform.localScale = new Vector3(newScale, newScale, newScale);

        }   
    }

	private float ConvertDistanceToScale(float maxDistance, float actualDistance, float planetScale)
    {
		var minDistance = maxDistance * -1;
		//the size of the planet scale can vary by half its scale so for a 0.1 scale sprite it will grow and shrink 0.05 either site
		var maxScale = planetScale / 2;
		var minScale = maxScale * -1;
		return (((actualDistance - minDistance) * (maxScale - minScale)) / (maxDistance - minDistance)) + minScale;
    }

}
