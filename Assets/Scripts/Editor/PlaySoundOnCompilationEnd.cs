using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Editor
{
	internal static class EditorSoundsTool
	{
		private const string audioClipResoPath = "CompilationSound";
		
		[DidReloadScripts]
		private static void PlaySoundOnCompilationEnd()
		{
			var clip = Resources.Load<AudioClip>(audioClipResoPath);
			if (!clip) return;
			PlayClip(clip);
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