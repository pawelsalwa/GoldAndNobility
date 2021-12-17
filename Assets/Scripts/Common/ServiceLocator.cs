using System;
using System.Collections.Generic;

namespace Common
{
	/// <summary> Manager for services instead of singletons to support interfaces and unit/integration tests. </summary>
	public class ServiceLocator
	{
		private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

		private static readonly ServiceLocator instance = new ServiceLocator();

		public static void RegisterService<T>(T service) where T : class
		{
			instance.services[typeof(T)] = service; // because unity calls mb constructors multiple times god knows why
		}
		
		public static void RegisterService(Type type, object service)
		{
			if (!type.IsInstanceOfType(service)) 
				throw new Exception($"Cannot register object of type {service.GetType().Name} as service of type {type.Name}");
			if (type.IsValueType) 
				throw new Exception($"Cannot register value type as service: {type.Name}");
			instance.services[type] = service;
		}

		public static T RequestService<T>() where T : class
		{
			if (!instance.services.ContainsKey(typeof(T))) throw new ArgumentException($"No service of type {typeof(T).Name} has been registered");
				// throw new Exception($"Requested service of type {typeof(T).Name} has not been registered!");
			return instance.services[typeof(T)] as T;
		}

		public static void MockServiceForTests<T>(T service) where T : class 
			=> instance.services[typeof(T)] = service; // override existing or add
	}
}