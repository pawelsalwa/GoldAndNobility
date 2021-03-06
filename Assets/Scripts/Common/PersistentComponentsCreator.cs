using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Attributes;
using Common.Utility;
using UnityEngine;

namespace Common
{
	/// <summary> We might want to refacor it to "GameService" or create game services in seprated scene for persistent gameobjects </summary>
	internal static class PersistentComponentsCreator
	{
		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void CreatePersistentComponents()
		{
			foreach (var type in GetPersistentComponentTypes())
				CreatePersistentComponent(type);
		}

		private static void CreatePersistentComponent(Type type)
		{
			if (!typeof(Component).IsAssignableFrom(type))
				throw new Exception($"Type {type.Name} shouldn't be marked with PersistentComponentAttribute because it doesn't inherit from Component!");
			
			var service = GameUtility.CreatePersistentComponent(type);
			CheckIfIsGameService(type, service);
		}

		private static void CheckIfIsGameService(Type type, Component service)
		{
			Type[] serviceTypes = null;
			var att = type.GetCustomAttribute<GameServiceAttribute>();
			if (att != null) serviceTypes = att.serviceTypes;
			if (serviceTypes == null || serviceTypes.Length <= 0) return;
			foreach (var serviceType in serviceTypes)
				ServiceLocator.RegisterService(serviceType, service);
		}

		private static IEnumerable<Type> GetPersistentComponentTypes()
		{
			return 
				from assembly in AppDomain.CurrentDomain.GetAssemblies() 
				from type in assembly.GetTypes() 
				where type.GetCustomAttributes(typeof(GameServiceAttribute), true).Length > 0 select type;
		}
	}
}

