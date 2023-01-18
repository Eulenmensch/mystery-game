using UnityEngine;
using System;

public class CameraEventSystem : MonoBehaviour
{
	#region Singleton
	public static CameraEventSystem Instance { get; private set; }

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

	public event Action<float> OnZoom;
	public void UpdateZoomValue(float _zoomValue){OnZoom?.Invoke(_zoomValue);}
	
}
