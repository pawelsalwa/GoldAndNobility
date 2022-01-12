using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    /// <summary> Initializes classes marked with [GameService] </summary>
    internal class GameServicesCreator : MonoBehaviour
    {
        private void Awake()
        {
            // if (!SceneManager.SetActiveScene(gameObject.scene)) throw new Exception("Scene error"); not needed, we wont use new gameobjects i guess
            CreateGameServices();
        }

        private void CreateGameServices()
        {
            var serviceTypes = GetGameServiceTypes();
            foreach (var type in serviceTypes) 
                RegisterTypeAsService(type);
        }

        private void RegisterTypeAsService(Type type)
        {
            var obj = CreateObjectOrComponent(type);
            TryRegisterAsService(type, obj);
        }

        private static void TryRegisterAsService(Type type, object service)
        {
            Type[] serviceTypes = null;
            var att = type.GetCustomAttribute<GameServiceAttribute>();
            if (att != null) serviceTypes = att.serviceTypes;
            if (serviceTypes == null || serviceTypes.Length <= 0) return;
            foreach (var serviceType in serviceTypes)
                ServiceLocator.RegisterService(serviceType, service);
        }

        private static IEnumerable<Type> GetGameServiceTypes() => Application.isEditor ? EditorGetServiceTypes() : BuildGetServiceTypes();

        private static IEnumerable<Type> BuildGetServiceTypes()
        {
            return 
                from assembly in AppDomain.CurrentDomain.GetAssemblies() 
                from type in assembly.GetTypes() 
                where type.GetCustomAttributes(typeof(GameServiceAttribute), true).Length > 0 select type;
        }

        /// <summary> Faster version for editor, using dedicated API </summary>
        private static IEnumerable<Type> EditorGetServiceTypes()
        {
#if  UNITY_EDITOR
            return UnityEditor.TypeCache.GetTypesWithAttribute<GameServiceAttribute>();
#endif
        }

        private object CreateObjectOrComponent(Type type)
        {
            object obj;
            var isComponent = typeof(Component).IsAssignableFrom(type);
            if (isComponent)
            {
                obj = gameObject.AddComponent(type);
            }
            else
            {
                var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                var hasParameterlessConstructor = constructor == null;
                if (!hasParameterlessConstructor) throw new TypeLoadException($"<color=red>{type.Name} doesnt have parameterless constructor, thus shouldn't be marked with [GameServiceAttribute] </color>");
                obj = Activator.CreateInstance(type);
            }
            return obj;
        }
    }
}