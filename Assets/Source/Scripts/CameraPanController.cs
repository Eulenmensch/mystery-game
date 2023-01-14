using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class CameraPanController : MonoBehaviour
{
    [field: SerializeField] private float panSmoothingTime;
    [SerializeField] private BoxCollider2D bounds;
    

    private bool isPressed;
    private Vector2 lastPos;
    private Vector2 cameraInertia;
    private Camera mainCamera;
    private TweenerCore<Vector2, Vector2, VectorOptions> tween;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        ApplyCameraInertia();
        HandleCameraBounds();
    }

    private void ApplyCameraInertia()
    {
        transform.position += (Vector3)cameraInertia * Time.deltaTime;
    }

    private void Pan(Vector2 _delta)
    {
        transform.position += (Vector3)_delta;
    }

    private void HandleCameraInertia(Vector2 _lastVelocity)
    {
        cameraInertia = _lastVelocity;
        tween = DOTween.To(() => cameraInertia, x => cameraInertia = x, Vector2.zero, panSmoothingTime);
    }

    private void HandleCameraBounds()
    {
        var position = transform.position;
        
        if (!bounds.OverlapPoint(transform.position))
        {
            tween.Kill();
            cameraInertia = Vector2.zero;
            transform.position = bounds.ClosestPoint(position);
        }
    }

    public void HandleDragInput(InputAction.CallbackContext _context)
    {
        var currentPos = _context.ReadValue<Vector2>();
        Vector2 delta = lastPos - (Vector2)mainCamera.ScreenToWorldPoint(currentPos);

        if (isPressed)
        {
            Pan(delta);
        }
    }

    public void HandlePressInput(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            tween.Kill();
            cameraInertia = Vector2.zero;
            isPressed = true;
            lastPos = Pointer.current.position.ReadValue();
            lastPos = mainCamera.ScreenToWorldPoint(lastPos);
        }

        if (_context.canceled)
        {
            isPressed = false;
            HandleCameraInertia(mainCamera.velocity);
        }
    }
}
