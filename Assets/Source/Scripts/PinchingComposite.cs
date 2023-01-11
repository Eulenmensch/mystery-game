//Solution found at https://forum.unity.com/threads/implementing-pinching.1369809/

#if UNITY_EDITOR
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
 
[InitializeOnLoad]
#endif

public class PinchingComposite : InputBindingComposite<float>
{
	[InputControl(layout = "Value")] public int FirstTouch;
	[InputControl(layout = "Value")] public int SecondTouch;

	public float NegativeScale = 1f;
	public float PositiveScale = 1f;

	private struct TouchStateComparer : IComparer<TouchState>
	{
		public int Compare(TouchState _x, TouchState _y) => 1;
	}
	
	//This method computes the resulting input value of the composite based
	//on the input from its part bindings.
	public override float ReadValue(ref InputBindingCompositeContext _context)
	{
		var touch0 = _context.ReadValue<TouchState, TouchStateComparer>(FirstTouch);
		var touch1 = _context.ReadValue<TouchState, TouchStateComparer>(SecondTouch);

		if (touch0.phase != TouchPhase.Moved || touch1.phase != TouchPhase.Moved)
		{
			return 0f;
		}

		var startDistance = math.distance(touch0.startPosition, touch1.startPosition);
		var distance = math.distance(touch0.position, touch1.position);

		var unscaledValue = startDistance / distance - 1f; // startDistance divided by distance to invert value
		var scaledValue = unscaledValue * (unscaledValue < 0 ? NegativeScale : PositiveScale);
		return scaledValue;
	}
	
	//This method computes the actuation of the binding as a whole
	public override float EvaluateMagnitude(ref InputBindingCompositeContext _context) => 1f;

	static PinchingComposite() => InputSystem.RegisterBindingComposite<PinchingComposite>();
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Init(){} //Trigger static constructor
}
