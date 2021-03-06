#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Editor
{
	/// <summary> plays audio clip in editor on double click </summary>
	internal static class PlayAudioClipTool
	{
		[OnOpenAsset]
		private static bool CheckAudioClip(int instanceID, int line)
		{
			var obj = EditorUtility.InstanceIDToObject(instanceID);
			var clip = obj as AudioClip;
			if (!clip) return false;
			PlayClip(clip);
			return true; // not sure if should return true here
		}

		private static void PlayClip(AudioClip clip)
		{
			Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
			Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
			var xd = audioUtilClass.GetMethods();
			var stopMethod = audioUtilClass.GetMethod("StopAllPreviewClips", BindingFlags.Static | BindingFlags.Public);
			if (stopMethod != null) stopMethod.Invoke(null, new object[] { });
			var playMethod = audioUtilClass.GetMethod("PlayPreviewClip", BindingFlags.Static | BindingFlags.Public);
			if (playMethod == null) return;
			playMethod.Invoke(null, new object[] {clip, 0, false});
		}
	}
}
#endif