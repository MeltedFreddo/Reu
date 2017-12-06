using Assets.Code.Data;
using Assets.Code.EventHandlers;
using UnityEngine;

namespace Assets.Code.Behaviours
{
    public class PlaceBuildingBehaviour : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var mouselocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = SnapVectorXandY(new Vector3(mouselocation.x, mouselocation.y, 0), gameObject.GetComponent<BuildingBehaviour>().Building);
            
            //var thing = Mathf.Round()

            if (Input.GetMouseButtonUp(0))
            {
                //building is placed, add it to the colony and deregister this script
            }
            if (Input.GetMouseButtonUp(1))
            {
                //building placement cancelled, destroy this object
                Destroy(gameObject);
            }
        }

        public Vector3 SnapVectorXandY(Vector3 vectorToSnap, Building building)
        {
            var roundedVector =
                new Vector3(
                    Mathf.Floor(vectorToSnap.x * 2) / 2,
                    Mathf.Floor(vectorToSnap.y * 2) / 2,
                    vectorToSnap.z
                );
            var offsetRoundedVector =
                new Vector3(
                    roundedVector.x + 0.25f * building.WidthInTiles,
                    (roundedVector.y - 0.25f * building.HeightInTiles) + 0.5f,
                    roundedVector.z
                );

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("vec2snap" + vectorToSnap);
                Debug.Log("offset" + offsetRoundedVector);
            }

            return offsetRoundedVector;
        }
    }
}
