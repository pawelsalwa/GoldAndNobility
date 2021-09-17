using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace Common
{
	public static class Helpers
	{

		public static bool IsSceneLoaded(string name)
		{
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				var sceneAt = SceneManager.GetSceneAt(i);
				if (sceneAt.isLoaded && sceneAt.name == name)
					return true;
			}

			return false;
		}

		public static IEnumerable<T> GetFieldsOfType<T>(this object obj)
		{
			if (obj == null) return null;
			var fieldInfos = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField);
			var typedFieldInfos = fieldInfos.Where(m => m.FieldType == typeof(T));
			var fields = typedFieldInfos.Select(fi => (T) fi.GetValue(obj));
			return fields.Where(a => a != null);
		}
		
		public static IEnumerable<T> GetPropertiesOfType<T>(this object obj)
		{
			if (obj == null) return null;
			var propertyInfos = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField);
			var typedPropertyInfos = propertyInfos.Where(m => m.PropertyType == typeof(T));
			var properties = typedPropertyInfos.Select(pi => (T) pi.GetValue(obj));
			return properties.Where(a => a != null);
		}
	}
}