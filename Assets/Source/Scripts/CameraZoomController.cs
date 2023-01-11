using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoomController : MonoBehaviour
{
	[SerializeField] private float minZoom;
	[SerializeField] private float maxZoom;
	[SerializeField] private float zoomSmoothingTime;
	[SerializeField] private CinemachineVirtualCamera virtualCamera;

	private void Zoom(float _zoomAmount)
	{
		var cameraZoom = virtualCamera.m_Lens.OrthographicSize;
		cameraZoom -= _zoomAmount;
		if (cameraZoom > maxZoom)
		{
			cameraZoom = maxZoom;
			return;
		}

		if (cameraZoom < minZoom)
		{
			cameraZoom = minZoom;
			return;
		}

		virtualCamera.m_Lens.OrthographicSize = cameraZoom;
	}
	
	
	
	public void HandleZoomInput(InputAction.CallbackContext _context)
	{
		if (_context.performed)
		{
			Zoom(_context.ReadValue<float>());
		}
	}
}
