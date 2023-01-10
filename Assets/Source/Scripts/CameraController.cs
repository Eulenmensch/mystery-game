using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [field: SerializeField] private float panSmoothing;
    [field: SerializeField] private float zoomSmoothing;

    private bool isPressed;
    private Vector2 lastDelta;

    private void Update()
    {
        throw new NotImplementedException();
    }

    private void Pan(Vector2 _delta)
    {
        
    }

    public void HandleDragInput(InputAction.CallbackContext _context)
    {
        if (isPressed)
        {
            Pan(_context.ReadValue<Vector2>());
        }
    }

    public void HandlePressInput(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            isPressed = true;
        }

        if (_context.canceled)
        {
            isPressed = false;
        }
    }
}
