using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utility;
using UnityEngine;

namespace Common
{
	internal static class PersistentComponentsCreator
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void CreatePersistentComponents()
		{
			foreach (var type in GetPersistentComponentTypes())
			{
				if (typeof(Component).IsAssignableFrom(type))
				{
					GameUtility.CreatePersistentComponent(type);
				}
				else
				{
					Debug.Log($"<color=red> Type {type.Name} shouldn't be marked with PersistentComponentAttribute because it doesn't inherit from Component! </color>");
				}
			}
		}

		private static IEnumerable<Type> GetPersistentComponentTypes()
		{
			return 
				from assembly in AppDomain.CurrentDomain.GetAssemblies() 
				from type in assembly.GetTypes() 
				where type.GetCustomAttributes(typeof(PersistentComponentAttribute), true).Length > 0 select type;
		}
	}
}

