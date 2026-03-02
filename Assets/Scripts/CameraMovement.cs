using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 30f)]
    private float _cameraSpeed;

    [SerializeField]
    private Vector2 _panLimitX;
    [SerializeField]
    private Vector2 _panLimitY;

    private Camera _camera;

    private Vector3 _movement;

    [SerializeField]
    [Range(0f, 100f)]
    private float _zoomSpeed = 60;

    [SerializeField]
    [Range(0f, 6f)]
    private float _minZoom = 2;

    [SerializeField]
    [Range(25f, 50f)]
    private float _maxZoom = 40;

    [SerializeField]
    [Range(0f, 5f)]
    private float _zoomSmoothness = 4;

    [SerializeField]
    [Range(0f, 50f)]
    private float _rotationSpeed=25;

    private float _currentZoom;
    private float _cameraHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the standard values of my camera
        _movement = Vector3.zero;
        _camera = GetComponentInChildren<Camera>();
        _currentZoom = _camera.orthographicSize;
        _cameraHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        this._movement.x = Input.GetAxisRaw("Horizontal");
        this._movement.z = Input.GetAxisRaw("Vertical");

        //change the current zoom to be equal to the scroll (and smooth it)
        _currentZoom = Mathf.Clamp(_currentZoom - Input.mouseScrollDelta.y * _zoomSpeed * Time.deltaTime, _minZoom, _maxZoom);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _currentZoom, _zoomSmoothness * Time.deltaTime);

        //check for the rotation of camera
        if (Input.GetMouseButton(2))
        {
            float mouseDeltaX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, mouseDeltaX * _rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {        
        //move the camera relative to the rotation
        this.transform.position = this.transform.position + Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * (_movement * Time.fixedDeltaTime * _cameraSpeed);
        //get the movement limited by the map boarder
        this.transform.position = new Vector3(Mathf.Clamp(transform.position.x, _panLimitX.x, _panLimitX.y), _cameraHeight, Mathf.Clamp(transform.position.z, _panLimitX.x, _panLimitX.y));
    }
}
