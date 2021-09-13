using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Utility
{
    public static class ReflectionUtility
    {
        public static IEnumerable<T> GetFieldsOfType<T>(this object obj) where T : class
        {
            var allFields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField);
            var fieldInfos = allFields.Where(m => m.FieldType == typeof(T));
            var fields = fieldInfos.Select(fi => fi.GetValue(obj) as T);
            return fields.Where(a => a != null);
        }
    }
}
