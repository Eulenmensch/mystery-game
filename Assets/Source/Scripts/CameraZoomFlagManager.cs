using Sirenix.OdinInspector;
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
	[SerializeField, HorizontalGroup("Street View")]
	private VisibilityFlag streetViewFlag;
	[SerializeField, HorizontalGroup("Street View"), HideLabel, LabelWidth(20)]
	private float streetViewThreshold;

	[SerializeField, HorizontalGroup("Floor Plan View")]
	private VisibilityFlag floorPlanViewFlag;
	[SerializeField, HorizontalGroup("Floor Plan View"), HideLabel, LabelWidth(20)] 
	private float floorPlanViewThreshold;
	
	[SerializeField, HorizontalGroup("Interior View")] 
	private VisibilityFlag interiorViewFlag;
	[SerializeField, HorizontalGroup("Interior View"), HideLabel, LabelWidth(20)] 
	private float interiorViewThreshold;

	private VisibilityFlag[] flags;
	
	private void OnEnable()
	{
		CameraEventSystem.Instance.OnZoom += SetZoomFlags;
	}

	private void OnDisable()
	{
		CameraEventSystem.Instance.OnZoom -= SetZoomFlags;
	}

	private void Start()
	{
		flags = new[] { streetViewFlag, floorPlanViewFlag, interiorViewFlag };
	}

	private void SetZoomFlags(float _zoomValue)
	{
		if(_zoomValue >= streetViewThreshold)
			SetActiveFlag(streetViewFlag);
		else if (_zoomValue >= floorPlanViewThreshold)
			SetActiveFlag(floorPlanViewFlag);
		else 
			SetActiveFlag(interiorViewFlag);
	}

	private void SetActiveFlag(VisibilityFlag _activeFlag)
	{
		foreach (var flag in flags)
		{
			flag.SetFlag(flag == _activeFlag);
		}
	}
}
