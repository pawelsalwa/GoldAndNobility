using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Utility
{
	internal static class GameUtility
	{

		private static GameObject persistentGoCache;
		private static GameObject persistentGo => persistentGoCache == null ? persistentGoCache = new GameObject("-- PersistentComponents --") : persistentGoCache;


		public static void ExitGame() // todo show are u sure popup
		{
#if UNITY_EDITOR
			if (Application.isEditor)
				UnityEditor.EditorApplication.ExitPlaymode();
			else
#endif
				Application.Quit();
		}

		public static void CreatePersistentComponent<T>() where T : Component => 
			Object.DontDestroyOnLoad(persistentGo.AddComponent<T>());
		
		public static void CreatePersistentComponent(Type type) => Object.DontDestroyOnLoad(persistentGo.AddComponent(type));

	}
}