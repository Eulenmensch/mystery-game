using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class CameraZoomController : MonoBehaviour
{
	[SerializeField] private float minZoom;
	[SerializeField] private float maxZoom;
	[SerializeField] private float zoomSmoothingTime;
	[SerializeField] private CinemachineVirtualCamera virtualCamera;

	private float cameraZoom;

	private void Start()
	{
		cameraZoom = virtualCamera.m_Lens.OrthographicSize;
	}

	private void Zoom(float _zoomAmount)
	{
		cameraZoom = virtualCamera.m_Lens.OrthographicSize;
		var zoomTarget = cameraZoom - _zoomAmount;
		// cameraZoom -= _zoomAmount;
		if (zoomTarget > maxZoom)
		{
			DOTween.Kill(cameraZoom);
			cameraZoom = maxZoom;
			return;
		}

		if (zoomTarget < minZoom)
		{
			DOTween.Kill(cameraZoom);
			cameraZoom = minZoom;
			return;
		}

		DOTween.To(() => cameraZoom, x => cameraZoom = x, zoomTarget, zoomSmoothingTime).OnUpdate(
			() => CameraEventSystem.Instance.UpdateZoomValue(cameraZoom));
	}


	private void LateUpdate()
	{
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
