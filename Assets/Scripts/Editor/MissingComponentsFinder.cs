using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
	public static class MissingComponentsFinder
	{
		[MenuItem("FlyingFat/SelectMissingComponents")]
		private static void SelectMissingComponents()
		{
			var selectionGos =
				Object.FindObjectsOfType<GameObject>()
					.Where(go => go.GetComponents<Component>()
					.Any(comp => comp == null))
					.ToArray<GameObject>();

			Selection.objects = selectionGos;
		}
		
		[MenuItem("FlyingFat/RemoveMissingComponents")]
		private static void RemoveMissingComponents()
		{
			foreach (var go in Object.FindObjectsOfType<GameObject>())
				foreach (var comp in go.GetComponents<Component>())
					if (comp == null)
						Object.DestroyImmediate(comp);
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
	}
}