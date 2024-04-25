using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Vector2 lastFramePosition;
    private float defaultSpeed;
    private float yVelocity;
    private float zoom;

    [Header("Camera Settings")]
    [SerializeField] private float startingZoom;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float maxCameraZoom;
    [SerializeField] private float minCameraZoom;
    [SerializeField] private float zoomMultiplier;
    [SerializeField] private float zoomSmoothTimer;

    [Header("Camera Controls")]
    [SerializeField] private InputActionReference CameraPanningKeyboard;


    private void Start()
    {
        defaultSpeed = cameraSpeed;
        zoom = startingZoom;

        SetCameraPosition();
    }

    private void Update()
    {
        Vector2 currentFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Mouse Controls
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            Vector2 diff = lastFramePosition - currentFramePosition;
            Camera.main.transform.Translate(diff);
        }

        Vector2 panVector = CameraPanningKeyboard.action.ReadValue<Vector2>();
        Camera.main.transform.Translate(panVector * cameraSpeed * Time.deltaTime);

        //2x Speed while pressing shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraSpeed = defaultSpeed * 2;
        }
        else
        {
            cameraSpeed = defaultSpeed;
        }

        //Scrolling
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minCameraZoom, maxCameraZoom);
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, zoom, ref yVelocity, zoomSmoothTimer);

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetCameraPosition()
    {
        Vector2 mapSize = MapManager.instance.GetSize();
        transform.localPosition = new Vector3(mapSize.x / 2, mapSize.y / 2, -10);
    }
}
