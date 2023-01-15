using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class CameraPanController : MonoBehaviour
{
    [field: SerializeField] private float panSmoothingTime;
    [SerializeField] private BoxCollider2D bounds;
    [SerializeField] private int multiplier;
    
    private bool isPressed;
    private Vector2 dragStartPos;
    private Vector2 lastPos;
    private float lastFrame;
    private Camera mainCamera;
    private Vector2 cameraInertia;
    private Vector2 cameraVelocity;
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
        var currentFrame = Time.frameCount;
        Vector2 delta = dragStartPos - (Vector2)mainCamera.ScreenToWorldPoint(currentPos);
        // Debug.Log(delta);
        if (isPressed)
        {
            Pan(delta);
        }
        
        var mouseDelta = mainCamera.ScreenToWorldPoint(lastPos) - mainCamera.ScreenToWorldPoint(currentPos);
        var frameDelta = currentFrame - lastFrame;
        cameraVelocity = mouseDelta/frameDelta * multiplier;
        lastPos = currentPos;
        lastFrame = currentFrame;
    }

    public void HandlePressInput(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            tween.Kill();
            cameraInertia = Vector2.zero;
            isPressed = true;
            dragStartPos = Pointer.current.position.ReadValue();
            dragStartPos = mainCamera.ScreenToWorldPoint(dragStartPos);
        }

        if (_context.canceled)
        {
            isPressed = false;
            HandleCameraInertia(cameraVelocity);
        }
    }
}
