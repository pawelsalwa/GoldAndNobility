using System;
using UnityEngine;

namespace Common.Fsm
{
	internal class Fsm : IFsm
	{
		public event Action<IState> OnStateChanged;
		public IState CurrentState { get; private set; }
		
		private bool enableDebugs = true;

		public void RequestStateChange(IState targetState)
		{
			if (enableDebugs) Debug.Log($"State changed from <color=cyan><b>{CurrentState?.GetType().Name}</b></color>, to <color=cyan><b>{targetState?.GetType().Name}</b></color>");
			CurrentState?.Exit();
			CurrentState = targetState;
			CurrentState?.Enter();
			OnStateChanged?.Invoke(CurrentState);
		}
	}
}