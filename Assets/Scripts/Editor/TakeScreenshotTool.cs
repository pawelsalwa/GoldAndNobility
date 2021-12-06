using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    internal class TakeScreenshotTool : EditorWindow
    {

        public Camera cam;
        [SerializeField] private string fullPath;
        private Vector2Int texSize = new(1920, 1080);

        [MenuItem("Tools/TakeScreenshotTool")]
        private static void GetWindow() => EditorWindow.GetWindow<TakeScreenshotTool>();

        private void OnGUI()
        {
            GUILayout.Label("below enter full path of file with extension (like .png) as if copied from windows file explorer");
            fullPath = GUILayout.TextField(fullPath);
            cam = EditorGUILayout.ObjectField(cam, typeof(Camera), true ) as Camera;
            texSize = EditorGUILayout.Vector2IntField("tex size", texSize);
            // EditorGUILayout.PropertyField(prop);
            if (GUILayout.Button("TakeScreenshot"))
            {
                TakeScreenshot();
            }
        }

        private void TakeScreenshot()
        {
            var rt = new RenderTexture(texSize.x, texSize.y, 24);
            cam.targetTexture = rt;
            var screenShot = new Texture2D(texSize.x, texSize.y, TextureFormat.RGBA32, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, texSize.x, texSize.y), 0, 0);
            cam.targetTexture = null;
            RenderTexture.active = null;
            if (Application.isEditor) DestroyImmediate(rt);
            else Destroy(rt);

            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes(fullPath, bytes);

            AssetDatabase.Refresh();
        }
    }
}
