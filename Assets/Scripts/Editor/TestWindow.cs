using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace Editor
{
	public class TestWindow : GraphViewEditorWindow
	{

		[MenuItem("XDD/test")]
		private static void Test()
		{
			ShowGraphViewWindowWithTools<GraphViewEditorWindow>();
		}
		
	}
}