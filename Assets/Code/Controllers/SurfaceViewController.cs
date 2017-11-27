using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.Code.BaseClasses;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class SurfaceViewController : AppMonoBehaviour
    {
        public List<GameObject> BuildingPrefabs;

        void Awake()
        {
            
        }

        // Use this for initialization
        void Start()
        {
            var currentColony = App.Instance.CurrentColony;

            if (currentColony == null)
            {
                //TODO get rid of this
                currentColony  = new Data.Colony {
                    Buildings = new List<Data.Building> {
                        new Data.Building { BuildingType = Data.Lists.BuildingType.Residence, IsActive = true, PowerConsumption = 1, X = 0, Y = 0 },
                        new Data.Building { BuildingType = Data.Lists.BuildingType.Park, IsActive = true, PowerConsumption = 1, X = 1, Y = 1 },
                    }   
                };
            }

            if (currentColony != null)
            {



                var allBuildings = currentColony.Buildings.ToList();
                for (var i = 0; i < allBuildings.Count; i++)
                {
                    var thisBuilding = allBuildings.ElementAt(i);
                    var buildingPrefab = BuildingPrefabs.Single(x => x.name == thisBuilding.BuildingType);
                    var pos = new Vector3(thisBuilding.X, thisBuilding.Y, 0);
                    Instantiate(buildingPrefab, pos, Quaternion.identity);
                }
            }
            else
            {
                
            }
                
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
