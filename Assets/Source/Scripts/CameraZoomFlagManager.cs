using System;
using UnityEngine;

public class CameraZoomFlagManager : MonoBehaviour
{
	#region Singleton
	public static CameraZoomFlagManager Instance { get; private set; }

	private void Awake()
	{
		if ( Instance != null && Instance != this )
		{
			Destroy( this );
		}
		else
		{
			Instance = this;
		}
	}
	#endregion

	private void OnEnable()
	{
		CameraEventSystem.Instance.OnZoom += SetZoomFlags;
	}

	private void OnDisable()
	{
		CameraEventSystem.Instance.OnZoom -= SetZoomFlags;
	}

	private void SetZoomFlags(float _zoomValue)
	{
		Debug.Log(_zoomValue);
	}
}
