using System;
using Cinemachine;
using UI;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Editor
{
	internal static class HierarchyExtensionTool
	{
		private const string ToolActivePropertyName = "HierarchyExtensionTool.toolActive";
		private const string StartServerInEditor = "HierarchyExtensionTool.startServerInEditor";
		private const string EnableToolTxt = "Turn on";
		private const string DisableToolTxt = "Turn off";

		private const int UiLayer = 5;

		private static bool colorThis = true;
		private static bool toolActive = false;

		// private static GUIStyle visibilityStyle = new GUIStyle {normal = new GUIStyleState {background = backgroundTex}};

		private static Texture2D _backgroundTex = new Texture2D(1, 1);
		private static Texture2D backgroundTex => _backgroundTex == null ? _backgroundTex = new Texture2D(1, 1) : _backgroundTex;
		
		private static Texture2D _greenTex = null;
		private static Texture2D greenTex
		{
			get
			{
				if (_greenTex) return _greenTex;
				_greenTex = new Texture2D(1, 1);
				_greenTex.SetPixel(0, 0, Color.green);
				_greenTex.Apply();
				return _greenTex;
			}
		}
		
		private static Texture2D _grayTex = null;
		// private static GUIStyle style = new GUIStyle{normal = new GUIStyleState {textColor = Color.cyan, background = grayTex}};

		private static Texture2D grayTex
		{
			get
			{
				if (_grayTex) return _grayTex;
				_grayTex = new Texture2D(1, 1);
				_grayTex.SetPixel(0, 0, Color.gray);
				_grayTex.Apply();
				return _grayTex;
			}
		}

		[InitializeOnLoadMethod]
		public static void Init()
		{
			EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyItemDrawWrapper;
			EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyItemDrawWrapper;

			EditorApplication.update -= CheckToolActivation;
			EditorApplication.update += CheckToolActivation;

			void CheckToolActivation() => toolActive = EditorPrefs.GetBool(ToolActivePropertyName);
		}

		private static void OnHierarchyItemDrawWrapper(int instanceid, Rect selectionrect)
		{
			try
			{
				OnHierarchyItemDraw(instanceid, selectionrect);
			} catch (Exception e)
			{
				if (e is ExitGUIException) throw;
				Debug.Log($"<color=white> [HierarchyToolError] Lets see stack trace... \n{e}</color>");
			}
		}

		private static void OnHierarchyItemDraw(int instanceid, Rect selectionrect)
		{
			// Debug.Log($"<color=white>onhierarchy draw {instanceid}</color>");
			var gameObj = (GameObject) EditorUtility.InstanceIDToObject(instanceid);
			if (gameObj == null)
			{
				DrawFancyOptsButtons(selectionrect);
				return;
			}

			if (!toolActive) return;
			DrawToggle(selectionrect, gameObj);
			TryDrawUiToggle(selectionrect, gameObj);
			DrawVCamToggle(selectionrect, gameObj);
			DrawBackground(selectionrect);
		}

		private static void DrawToggle(Rect selectionrect, GameObject gameObj)
		{
			var toggleRect = new Rect {x = selectionrect.xMax - 35, y = selectionrect.yMin, width = 15, height = 15f};
			var activated = GUI.Toggle(toggleRect, gameObj.activeSelf, String.Empty);

			if (activated == gameObj.activeSelf) return;
			gameObj.SetActive(!gameObj.activeSelf);
			if (!Application.isPlaying)
				MarkSceneDirty(gameObj.scene);
		}
		
		private static void TryDrawUiToggle(Rect selectionrect, GameObject gameObj)
		{
			if (gameObj.layer != UiLayer) return;
			var visionToggle = gameObj.GetComponent<IVisibilityToggle>();
			if (visionToggle == null) return;
			var toggleRect = new Rect {x = selectionrect.xMax - 50f, y = selectionrect.yMin, width = 15, height = 15f};
			var cache = GUI.color;
			GUI.color = Color.cyan;
			var visible = GUI.Toggle(toggleRect, visionToggle.Visible, backgroundTex);
			GUI.color = cache;

			if (visible && !visionToggle.Visible)
			{
				visionToggle.Show();
				if (!Application.isPlaying) MarkSceneDirty(gameObj.scene);
			}

			if (!visible && visionToggle.Visible)
			{
				visionToggle.Hide();
				if (!Application.isPlaying) MarkSceneDirty(gameObj.scene);
			}
			
		}

		private static void DrawVCamToggle(Rect selectionrect, GameObject gameObj)
		{
			if (!gameObj.activeInHierarchy) return;
			var targetVCam = gameObj.GetComponent<CinemachineVirtualCameraBase>();
			if (!targetVCam) return;
			var toggleRect = new Rect {x = selectionrect.xMax - 55f, y = selectionrect.yMin, width = 15, height = 15f};

			var cache = GUI.backgroundColor; 
			GUI.backgroundColor = Color.cyan;
			if (GUI.Button(toggleRect, targetVCam.Priority == 0 ? "" : "+"))
			{
				foreach (var vCam in Object.FindObjectsOfType<CinemachineVirtualCameraBase>())
				{
					vCam.Priority = vCam == targetVCam ? 1 : 0;
				}
				if (!Application.isPlaying)
					MarkSceneDirty(gameObj.scene);
			}

			GUI.backgroundColor = cache;
			// if (activated == gameObj.activeSelf) return;
			// gameObj.SetActive(!gameObj.activeSelf);
		}

		private static void MarkSceneDirty(Scene scene)
		{
			EditorSceneManager.MarkSceneDirty(scene);
			var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
			if (prefabStage != null)
				EditorSceneManager.MarkSceneDirty(prefabStage.scene);
		}

		private static void DrawBackground(Rect selectionrect)
		{
			backgroundTex.SetPixel(0, 0, new Color(0f, 0.5f + (selectionrect.yMin / Screen.height) * 0.26f, 0.7f - selectionrect.yMin / Screen.height, colorThis ? 0.53f : 0.03f));
			backgroundTex.Apply();
			var rect = new Rect {xMin = Screen.safeArea.xMin + 32f, xMax = Screen.width, yMin = selectionrect.yMax - 0f, yMax = selectionrect.yMax + 1f};
			// var rect = new Rect {xMin = Screen.safeArea.xMin + 32f, xMax = Screen.width, yMin = selectionrect.yMin, yMax = selectionrect.yMax};
			GUI.DrawTexture(rect, backgroundTex);
			// GUI.backgroundColor = new Color(0f, selectionrect.yMin / Screen.height, 1f - selectionrect.yMin / Screen.height, colorThis ? 0.13f : 0.03f);
			//colorThis = !colorThis; // toggling drawing isnt good - unity draws selected item first so when selection changes this tool glitters
		}

		private static void DrawFancyOptsButtons(Rect rect)
		{
			var activationBtnRect = new Rect {xMin = rect.xMax - 62f, xMax = rect.xMax - 5f, yMin = rect.yMin, yMax = rect.yMax};
			if (GUI.Button(activationBtnRect, toolActive ? DisableToolTxt : EnableToolTxt))
				EditorPrefs.SetBool(ToolActivePropertyName, !toolActive);

			var quickLoadRect = new Rect {xMin = activationBtnRect.x - 80f, xMax = activationBtnRect.x - 5f, yMin = activationBtnRect.yMin, yMax = activationBtnRect.yMax};
			var guiEnabledCache = GUI.enabled;
			GUI.enabled = !Application.isPlaying;
			if (GUI.Button(quickLoadRect, "QuickLoad"))
			{
				PopupWindow.Show(quickLoadRect, new SceneChoosePopupContent());
			}
			
			GUI.enabled = guiEnabledCache;
			
			// var playModeSetupRect = quickLoadRect;
			// playModeSetupRect.center = quickLoadRect.center + Vector2.left * 40f;
			// playModeSetupRect.width = 35f;
			// if (GUI.Button(playModeSetupRect, "Cfg"))
			// {
			// 	// PopupWindow.Show(playModeSetupRect, new PlayConfigurationPopup());
			// }
		}
	}
}