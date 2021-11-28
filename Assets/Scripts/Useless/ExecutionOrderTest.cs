using System;
using Common.Attributes;
using UnityEngine;

namespace Common.Fsm
{
	internal class ExecutionOrderTest : MonoBehaviour
	{
		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init() => Debug.Log($"<color=blue>static before scene load</color>");

		private ExecutionOrderTest() => Debug.Log($"<color=cyan>MB Constructor</color>");
		private void Awake() => Debug.Log($"<color=green>MB Awake</color>");
		private void Start() => Debug.Log($"<color=orange>MB Start</color>");
		private void OnDestroy() => Debug.Log($"<color=red>MB OnDestroy</color>");
	}

	// [PersistentComponent]
	internal class ExecutionOrderTestPersistent : MonoBehaviour
	{
		private ExecutionOrderTestPersistent() => Debug.Log($"<color=cyan><b>Persistent Constructor </b></color>");
		private void Awake() => Debug.Log($"<color=green><b>Persistent Awake </b></color>");
		private void Start() => Debug.Log($"<color=orange><b>Persistent Start </b></color>");
		private void OnDestroy() => Debug.Log($"<color=red><b>Persistent OnDestroy </b></color>");
	}
	
	// [PersistentComponent]
	internal class ExecutionOrderTestPersistent2nd : MonoBehaviour
	{
		private ExecutionOrderTestPersistent2nd() => Debug.Log($"<color=cyan><b>Persistent Constructor 2nd version!</b></color>");
		private void Awake() => Debug.Log($"<color=green><b>Persistent Awake 2nd version!</b></color>");
		private void Start() => Debug.Log($"<color=orange><b>Persistent Start 2nd version!</b></color>");
		private void OnDestroy() => Debug.Log($"<color=red><b>Persistent OnDestroy 2nd version!</b></color>");
	}
}