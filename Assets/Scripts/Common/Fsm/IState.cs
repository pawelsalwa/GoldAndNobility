using System;

namespace Common.Fsm
{
	public interface IState
	{
		event Action OnEntered;
		event Action OnExited;
		
		void Enter();
		void Exit();
		void Awake();
	}
}