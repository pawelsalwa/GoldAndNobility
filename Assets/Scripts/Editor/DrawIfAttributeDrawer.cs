// using Common;
// using Common.Attributes;
// using UnityEditor;
// using UnityEngine;
//
// namespace Editor
// {
// 	[CustomPropertyDrawer(typeof(DrawIfAttribute))]
// 	internal class DrawIfAttributeDrawer : PropertyDrawer
// 	{
//
// 		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
// 		{
// 			EditorGUI.PropertyField(position, property, label);
// 			var targetObj = property.GetTargetObjectOfProperty();
// 			var unityObj = property.serializedObject;
// 			var fields = targetObj.GetFieldsOfType<bool>();
// 			
// 		}
//
// 		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
// 		{
// 			return EditorGUIUtility.singleLineHeight;
// 		}
//
// 	}
// }