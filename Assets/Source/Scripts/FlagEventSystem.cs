using UnityEngine;
using System;

public class FlagEventSystem : MonoBehaviour
{
	#region Singleton
	public static FlagEventSystem Instance { get; private set; }

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

	public event Action<VisibilityFlag,bool> OnFlagChanged;
	public void ChangeFlag(VisibilityFlag _flag, bool _isTrue){OnFlagChanged?.Invoke(_flag,_isTrue);}
}
