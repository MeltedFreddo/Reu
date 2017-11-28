using UnityEngine;

namespace Assets.Code.EventHandlers
{
    public class CameraHandler : MonoBehaviour
    {
        private bool _isMouseMovingCamera;
        private Vector3 _originalMousePosition;

        // Use this for initialization
        void Start () {
		
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
                Camera.main.orthographicSize -= .1f;
            }
            if (Input.GetKey(KeyCode.KeypadMinus))
            {
                Camera.main.orthographicSize += .1f;
            }
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
                Camera.main.orthographicSize -= .3f;
            }
            else if (y <= -1)
            {
                Camera.main.orthographicSize += .3f; 
            }
        }
    }
}