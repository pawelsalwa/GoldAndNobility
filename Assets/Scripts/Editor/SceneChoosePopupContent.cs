using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
	public class SceneChoosePopupContent : PopupWindowContent
	{
		private const string LoadAsSinglePropertyName = "HierarchyExtensionTool.LoadAsSingle";
		
		private bool loadAsSingle = false;
		private List<SceneAsset> sceneAssets;
		private Vector2 scrollPosition;

		public override void OnGUI(Rect rect)
		{
			loadAsSingle = GUILayout.Toggle(loadAsSingle, "load as single");
			EditorPrefs.SetBool(LoadAsSinglePropertyName, loadAsSingle);
			DrawSceneBtns();
		}

		private void DrawSceneBtns()
		{
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
			foreach (var sceneAsset in sceneAssets)
			{
				if (!GUILayout.Button(sceneAsset.name)) continue;
				List<Scene> scenes = new List<Scene>();
				for (int i = 0; i < SceneManager.sceneCount; i++) 
					scenes.Add(SceneManager.GetSceneAt(i));
				
				if (loadAsSingle) EditorSceneManager.SaveModifiedScenesIfUserWantsTo(scenes.ToArray());
				var path = AssetDatabase.GetAssetPath(sceneAsset);
				EditorSceneManager.OpenScene(path, loadAsSingle ? OpenSceneMode.Single : OpenSceneMode.Additive);
				editorWindow.Close();
			}

			GUILayout.EndScrollView();
		}

		public override void OnOpen()
		{
			loadAsSingle = EditorPrefs.GetBool(LoadAsSinglePropertyName, loadAsSingle);
			
			var guids = AssetDatabase.FindAssets("t:scene", new[] {"Assets/Scenes"}); // lets use one folder for scenes for now
			sceneAssets = guids.Select(GetAssetFromGuid).ToList();
			
			SceneAsset GetAssetFromGuid(string guid) => AssetDatabase.LoadAssetAtPath<SceneAsset>(AssetDatabase.GUIDToAssetPath(guid));
		}

		public override Vector2 GetWindowSize()
		{
			var height = Mathf.Max(200f, Mathf.Min(sceneAssets.Count * EditorGUIUtility.singleLineHeight, 400f));
			return new Vector2(base.GetWindowSize().x, height);
		}
	}
}