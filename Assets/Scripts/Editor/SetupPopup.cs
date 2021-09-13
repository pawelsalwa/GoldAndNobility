using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace Editor
{
	internal class SetupPopup : PopupWindowContent
	{
		[Shortcut("SetupPopup", KeyCode.Y)]
		private static void DatenshiToolsPopup()
		{
			var mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            
        
			var actualScreenPosition = new Vector2(
				Event.current.mousePosition.x,
				// the Y position is flipped, so we have to account for that
				// we also have to account for parts above the "Scene" window
				Screen.height - (Event.current.mousePosition.y + 25)
			);
            
			var rect = new Rect(mousePos.x, mousePos.y, 200, 200);
			Debug.Log($"<color=orange> mouse pos {mousePos} </color>");
			Debug.Log($"<color=white> mouse pos fix{actualScreenPosition} </color>");

			rect = new Rect(Screen.width / 2f, Screen.height / 2f, 200, 200);
			PopupWindow.Show(rect, new SetupPopup());
		}
        
        
		public override void OnGUI(Rect rect)
		{
			GUI.Label(rect, "to dziala xd");
			var actualScreenPosition = new Vector2(Event.current.mousePosition.x, Screen.height - (Event.current.mousePosition.y + 25));
			// Debug.Log($"<color=white> mouse pos {actualScreenPosition} </color>");
		}
	}
}
