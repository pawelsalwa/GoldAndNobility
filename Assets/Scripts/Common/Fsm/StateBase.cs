using System;
using System.Linq;

namespace Common.Fsm
{
	internal abstract class StateBase : IState
	{
		private event Action OnEnteredCache;
		public event Action OnEntered
		{
			add
			{
				if (OnEnteredCache == null || !OnEnteredCache.GetInvocationList().Contains(value)) OnEnteredCache += value; // so we can omit unsub everytime
			}
			remove => OnEnteredCache -= value;
		}
		
		private event Action OnExitedCache;
		public event Action OnExited
		{
			add
			{
				if (OnExitedCache == null || !OnExitedCache.GetInvocationList().Contains(value)) OnExitedCache += value;
			}
			remove => OnExitedCache -= value;
		}

		public void Enter()
		{
			OnEnter();
			OnEnteredCache?.Invoke();
		}

		public void Exit()
		{
			OnExit();
			OnExitedCache?.Invoke();
		}

		public void Awake() => OnAwake();

		protected virtual void OnAwake() { }
		protected virtual void OnEnter() { }
		protected virtual void OnExit() { }
	}
}