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
        
        public static IEnumerable<T> GetFieldsOfType<T>(this object obj) where T : class
        {
            var fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField);
            var fieldInfos = fields.Where(m => m.FieldType == typeof(T));
            var abilities = fieldInfos.Select(fi => fi.GetValue(obj) as T);
            return abilities.Where(a => a != null);
        }
    }
}
