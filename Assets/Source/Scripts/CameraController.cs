using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [field: SerializeField] private float panSmoothing;
    [field: SerializeField] private float zoomSmoothing;

    private bool isPressed;
    private Vector2 lastPos;
    private float cameraInertia;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Pan(Vector2 _delta)
    {
        transform.position += (Vector3)_delta;
    }

    public void HandleDragInput(InputAction.CallbackContext _context)
    {
        var currentPos = _context.ReadValue<Vector2>();
        Vector2 delta = lastPos - (Vector2)mainCamera.ScreenToWorldPoint(currentPos);
        // Debug.Log("origin " + lastPos + " to current " + currentPos + " equals a delta of " + delta);

        if (isPressed)
        {
            Pan(delta);
        }
    }

    public void HandlePressInput(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            isPressed = true;
            lastPos = Pointer.current.position.ReadValue();
            lastPos = mainCamera.ScreenToWorldPoint(lastPos);
        }

        if (_context.canceled)
        {
            isPressed = false;
        }
    }
}
