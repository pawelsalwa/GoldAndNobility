using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	/// <summary> Manager for services instead of singletons to support interfaces and unit/integration tests. </summary>
	public class ServiceLocator
	{
		private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

		private static readonly ServiceLocator instance = new ServiceLocator();

		public static Dictionary<Type, object> DebugServices => instance.services; // just for debug healthCheck

		private ServiceLocator() { }

		public static void RegisterService<T>(T service) where T : class
		{
			// if (instance.services.ContainsKey(typeof(T)))
			// {
			// 	Debug.LogError($"<color=red>You've trying to register the same type ({typeof(T).Name}) multiple times!</color>");
			// 	return;
			// }
			instance.services[typeof(T)] = service; // because unity calls mb constructors multiple times god knows why
		}

		public static T RequestService<T>() where T : class
		{
			if (!instance.services.ContainsKey(typeof(T)))
			{
				Debug.LogError($"<color=red>Requested service of type {typeof(T).Name} has not been registered! </color>");
				return null; 
			}
			return instance.services[typeof(T)] as T;
		}

		// public static void UnregisterService<T>(T service)
		// {
		// 	if (!instance.services.ContainsValue(service))
		// 	{
		// 		Debug.LogError($"<color=red>You're trying to unregister service of type {typeof(T).Name} that has not been registered! </color>");
		// 		return;
		// 	}
		// 	instance.services.Remove(typeof(T));
		// }

		public static void MockServiceForTests<T>(T service) where T : class 
			=> instance.services[typeof(T)] = service; // override existing or add
	}
}