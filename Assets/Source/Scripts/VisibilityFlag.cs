using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "VisibilityFlag", menuName = "VisibilityFlag", order = 0)]
public class VisibilityFlag : ScriptableObject
{
	[ShowInInspector, ReadOnly]
	private bool isTrue;

	public void SetFlag(bool _isTrue)
	{
		isTrue = _isTrue;
		FlagEventSystem.Instance.ChangeFlag(this, _isTrue);
	}

	public bool IsTrue()
	{
		return isTrue;
	}
}
