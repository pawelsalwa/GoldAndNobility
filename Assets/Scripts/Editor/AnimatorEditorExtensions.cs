using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;

namespace Editor
{
	public static class AnimatorEditorExtensions
	{

		public static List<AnimatorState> GetAllStates(AnimatorController animController)
		{
			var stateMachine = animController.layers[0].stateMachine; // fuck other layers
			var states = new List<AnimatorState>();
			AddAnimaStateRecursively(stateMachine, states);
			return states;
		}

		private static void AddAnimaStateRecursively(AnimatorStateMachine stateMachine, List<AnimatorState> states)
		{
			foreach (var childStateMachine in stateMachine.stateMachines) 
				AddAnimaStateRecursively(childStateMachine.stateMachine, states);
			
			states.AddRange(stateMachine.states.Select(s => s.state));
		}
	}
}