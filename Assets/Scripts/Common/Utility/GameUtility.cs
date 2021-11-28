using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Utility
{
	internal static class GameUtility
	{

		private static GameObject persistentGoCache;
		private static GameObject persistentGo
		{
			get
			{
				if (!persistentGoCache) Object.DontDestroyOnLoad(persistentGoCache = new GameObject("-- PersistentComponents --"));
				return persistentGoCache;
			}
		}


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
		
		public static Component CreatePersistentComponent(Type type) => persistentGo.AddComponent(type);
	}
}