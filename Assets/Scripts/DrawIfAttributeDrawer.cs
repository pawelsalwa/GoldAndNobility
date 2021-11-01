using System.Linq;
using Common;
using Editor;
using UnityEditor;
using UnityEngine;



	[CustomPropertyDrawer(typeof(DrawIfAttribute))]
	public class DrawIfAttributeDrawer : PropertyDrawer
	{

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property, label);
			var targetObj = property.GetTargetObjectOfProperty();
			var unityObj = property.serializedObject;
			var fields = targetObj.GetFieldsOfType<bool>().ToList();
			// property.serializedObject.
			
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}

	}