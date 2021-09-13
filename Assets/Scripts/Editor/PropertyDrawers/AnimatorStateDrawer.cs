using System.Collections.Generic;
using System.Linq;
using Common.Attributes;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Editor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof(AnimatorStateAttribute))]
	public class AnimatorStateDrawer : PropertyDrawer
	{
		private int idx = 0;
		private string[] statesDisplay;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.Integer)
			{
				// Debug.LogWarning("AnimatorStateAttribute can be used only on int fields.");
				EditorGUI.LabelField(position, "AnimatorStateAttribute can be used only on int fields.");
				// EditorGUI.PropertyField(position, property, label);
				return;
			}

			var animator = TryGetAnimatorFromNeighbourProperties(property); // if any serProperty references animator we use this animator here
			
			if (animator == null && property.serializedObject.targetObject is Component component)
				animator = component.GetComponent<Animator>(); // try find attached animator if not found yet

			if (animator == null || animator.runtimeAnimatorController == null)
				EditorGUI.LabelField(position, "Animator states not found");
			else
				DrawPopup(position, property, animator);
		}

		private static Animator TryGetAnimatorFromNeighbourProperties(SerializedProperty property)
		{
			var prop = property.serializedObject.GetIterator();
			prop.Next(true);
			bool hasNext = prop != null;

			while (hasNext)
			{
				var targetObj = prop.GetTargetObjectOfProperty();
				if (targetObj is Animator refedAnimator) return refedAnimator;
				hasNext = prop.Next(false);
			}

			return null;
		}

		private void DrawPopup(Rect position, SerializedProperty property, Animator animator)
		{
			var animatorController = animator.runtimeAnimatorController as AnimatorController;
			var states = AnimatorEditorExtensions.GetAllStates(animatorController);

			for (int i = 0; i < states.Count; i++)
				if (property.intValue == states[i].nameHash)
					idx = i;

			statesDisplay = states.Select(x => x.name).ToArray();
			idx = EditorGUI.Popup(position, property.displayName, idx, statesDisplay);
			idx = Mathf.Clamp(idx, 0, states.Count - 1);
			if (0 < idx  && idx < states.Count) property.intValue = states[idx].nameHash;
		}
		
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
}