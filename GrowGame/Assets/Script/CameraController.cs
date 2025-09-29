using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _moveRate = 30f;
    [SerializeField] private bool useBounds = false;
    [SerializeField] private float minX = -100f, maxX = 100f, minZ = -100f, maxZ = 100f;

    private InfraClick iClick;
    private Vector3 _startParentPos;
    private Vector3 _startMousePosView;
    private bool prevReturnFinished = false;
    private bool ignoreMouseHoldUntilRelease = false;
    private bool isDragging = false;

    void Awake()
    {
        if (_camera == null) _camera = Camera.main;
        var gm = GameObject.Find("GameManager");
        if (gm != null) iClick = gm.GetComponent<InfraClick>();
    }

    void Update()
    {
        if (iClick == null) return;

        if (iClick.returnFinished)
        {
            if (!prevReturnFinished)
            {
                prevReturnFinished = true;
                if (Input.GetMouseButton(0))
                {
                    ignoreMouseHoldUntilRelease = true;
                }
                else
                {
                    ignoreMouseHoldUntilRelease = false;
                }
            }

            if (Input.GetMouseButtonDown(0) && !ignoreMouseHoldUntilRelease)
            {
                _startMousePosView = _camera.ScreenToViewportPoint(Input.mousePosition);
                _startParentPos = transform.position;
                isDragging = true;
            }
            else if (Input.GetMouseButton(0) && isDragging)
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

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                ignoreMouseHoldUntilRelease = false;
            }
        }
        else
        {
            prevReturnFinished = false;
            ignoreMouseHoldUntilRelease = false;
            isDragging = false;
        }
    }
}
