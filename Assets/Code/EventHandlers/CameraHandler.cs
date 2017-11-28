using UnityEngine;

namespace Assets.Code.EventHandlers
{
    public class CameraHandler : MonoBehaviour
    {
        private bool _isMouseMovingCamera;
        private Vector3 _originalMousePosition;

		private int mapWidth = 50;
		private int mapHeight = 50;

		public float minZoom = 3f;
		private float maxZoom = 8f;

		private float minX, maxX, minY, maxY;

        // Use this for initialization
        void Start () {
			RecalculateScreenBoundaries();
        }
	
        // Update is called once per frame
        void Update () {
            var speed = 5.0f;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.KeypadPlus))
            {
				ZoomCamera(-.1f);
            }
            if (Input.GetKey(KeyCode.KeypadMinus))
            {
				ZoomCamera(.1f);
            }

			ClampCameraToBoundary();
        }

        void LateUpdate()
        {
            var offsetBetweenMouseAndCamera = new Vector3();
            
            if (Input.GetMouseButton(1))
            {
                offsetBetweenMouseAndCamera = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane))) - Camera.main.transform.position;
                if (!_isMouseMovingCamera)
                {
                    _isMouseMovingCamera = true;
                    _originalMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
                }
            }
            else
            {
                _isMouseMovingCamera = false;
            }
            if (_isMouseMovingCamera)
            {
                Camera.main.transform.position = _originalMousePosition - offsetBetweenMouseAndCamera;
            }

            var y = Input.mouseScrollDelta.y;
            if (y >= 1)
            {
				ZoomCamera(-.3f);
            }
            else if (y <= -1)
            {
				ZoomCamera(.3f);
            }
			ClampCameraToBoundary();
        }

		void RecalculateScreenBoundaries() {
			float effectiveCameraHeight = Camera.main.orthographicSize * 2.0f;
			float screenScale = effectiveCameraHeight * Screen.width / Screen.height;

			maxX = (mapWidth - screenScale) / 2.0f;
			maxY = (mapHeight - screenScale) / 2.0f;
			minX = -maxX;
			minY = -maxY;
		}

		private void ZoomCamera(float zoomDirectionAndRate)
		{
			Camera.main.orthographicSize += zoomDirectionAndRate; 
			Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, minZoom, maxZoom);
			RecalculateScreenBoundaries();
		}

		private void ClampCameraToBoundary()
		{
			Vector3 pos = transform.position;
			pos.x = Mathf.Clamp (pos.x, minX, maxX);
			pos.y = Mathf.Clamp (pos.y, minY, maxY);
			transform.position = pos;
		}
    }
}