using UnityEngine;

[CreateAssetMenu(fileName = "VisibilityFlag", menuName = "VisibilityFlag", order = 0)]
public class VisibilityFlag : ScriptableObject
{
	private bool isTrue;

	public void SetFlag(bool _isTrue)
	{
		isTrue = _isTrue;
	}

	public bool IsTrue()
	{
		return isTrue;
	}
}
