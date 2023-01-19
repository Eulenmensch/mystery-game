using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VisibilityController : MonoBehaviour
{
	[SerializeField] private VisibilityFlag flag;
	private SpriteRenderer sprite;
	private void OnEnable()
	{
		FlagEventSystem.Instance.OnFlagChanged += SetVisibility;
	}
	private void OnDisable()
	{
		FlagEventSystem.Instance.OnFlagChanged -= SetVisibility;
	}

	private void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	private void SetVisibility(VisibilityFlag _flag, bool _spriteVisible)
	{
		sprite.enabled = _spriteVisible;
	}
}
