using System;
using Common.Attributes;
using UnityEngine;

namespace Common.Fsm
{
	internal class ExecutionOrderTest : MonoBehaviour
	{
		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		// private static void BeforeSceneLoad() => Debug.Log($"<color=blue>static BeforeSceneLoad</color>");
		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		// private static void AfterSceneLoad() => Debug.Log($"<color=blue>static AfterSceneLoad</color>");
		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		// private static void SubsystemRegistration() => Debug.Log($"<color=blue>static SubsystemRegistration</color>");
		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		// private static void AfterAssembliesLoaded() => Debug.Log($"<color=blue>static AfterAssembliesLoaded</color>");
		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		// private static void BeforeSplashScreen() => Debug.Log($"<color=blue>static BeforeSplashScreen</color>");
 
		private ExecutionOrderTest() => Debug.Log($"<color=cyan>MB Constructor</color>");
		private void Awake() => Debug.Log($"<color=green>MB Awake</color>");
		private void Start() => Debug.Log($"<color=orange>MB Start</color>");
		private void OnDestroy() => Debug.Log($"<color=red>MB OnDestroy</color>");
	}

	// [GameService()]
	internal class ExecutionOrderTestPersistent : MonoBehaviour
	{
		private ExecutionOrderTestPersistent() => Debug.Log($"<color=cyan><b>Persistent Constructor </b></color>");
		private void Awake() => Debug.Log($"<color=green><b>Persistent Awake </b></color>");
		private void Start() => Debug.Log($"<color=orange><b>Persistent Start </b></color>");
		private void OnDestroy() => Debug.Log($"<color=red><b>Persistent OnDestroy </b></color>");
	}
	
	// [GameService]
	internal class ExecutionOrderTestPersistent2nd : MonoBehaviour
	{
		private ExecutionOrderTestPersistent2nd() => Debug.Log($"<color=cyan><b>Persistent Constructor 2nd version!</b></color>");
		private void Awake() => Debug.Log($"<color=green><b>Persistent Awake 2nd version!</b></color>");
		private void Start() => Debug.Log($"<color=orange><b>Persistent Start 2nd version!</b></color>");
		private void OnDestroy() => Debug.Log($"<color=red><b>Persistent OnDestroy 2nd version!</b></color>");
	}
}