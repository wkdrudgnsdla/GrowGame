using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _moveRate = 30f;
    [SerializeField] private bool useBounds = false;
    [SerializeField] private float minX = -100f, maxX = 100f, minZ = -100f, maxZ = 100f;

    private Vector3 _startParentPos;
    private Vector3 _startMousePosView;

    void Awake()
    {
        if (_camera == null) _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePosView = _camera.ScreenToViewportPoint(Input.mousePosition);
            _startParentPos = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 currentMouseView = _camera.ScreenToViewportPoint(Input.mousePosition);
            Vector3 deltaView = currentMouseView - _startMousePosView;
            Vector3 camRight = _camera.transform.right;
            camRight.y = 0f;
            camRight.Normalize();
            Vector3 camForward = _camera.transform.forward;
            camForward.y = 0f;
            camForward.Normalize();
            Vector3 worldMove = -(camRight * deltaView.x + camForward * deltaView.y) * _moveRate;
            Vector3 newPos = _startParentPos + worldMove;
            newPos.y = _startParentPos.y;
            if (useBounds)
            {
                newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
                newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);
            }
            transform.position = newPos;
        }
    }
}
