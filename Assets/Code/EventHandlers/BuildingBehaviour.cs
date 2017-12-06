using Assets.Code.Data;
using UnityEngine;

namespace Assets.Code.EventHandlers
{
    public class BuildingBehaviour : MonoBehaviour {

        public Building Building;

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {

            if (Input.GetMouseButtonUp(0))
            {
                //Converting Mouse Pos to 2D (vector2) World Pos
                Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                var hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

                if (hit)
                {
                    if (transform == hit.transform)
                    {
                        Debug.Log("Toggling building selection of " + hit.transform.name);
                        ToggleBuildingSelection();
                    }
                }
                else
                {
                    UnselectAllBuildings();
                }
            }
        }

        void UnselectAllBuildings()
        {
            var selectedOutlines = GameObject.FindGameObjectsWithTag("SelectedOutline");
            foreach (var outline in selectedOutlines)
            {
                Destroy(outline);
            }
        }

        void ToggleBuildingSelection()
        {
            var selectedOutline = transform.Find("SelectedOutline");
            if (selectedOutline != null)
            {
                Destroy(selectedOutline.gameObject);
            }
            else
            {
                UnselectAllBuildings();


                var selectedOutlineGameObject = Instantiate((GameObject)Resources.Load("Prefabs/Buildings/SelectedOutline"), transform.position, Quaternion.identity);
                selectedOutlineGameObject.name = "SelectedOutline";
                selectedOutlineGameObject.tag = "SelectedOutline";
                selectedOutlineGameObject.transform.parent = transform;
                var spriteRenderer = selectedOutlineGameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 1;

                selectedOutlineGameObject.transform.localScale =
                    new Vector3(
                        selectedOutlineGameObject.transform.localScale.x * Building.WidthInTiles,
                        selectedOutlineGameObject.transform.localScale.y * Building.HeightInTiles,
                        selectedOutlineGameObject.transform.localScale.z
                    );
            
            }
        
        }
    }
}
