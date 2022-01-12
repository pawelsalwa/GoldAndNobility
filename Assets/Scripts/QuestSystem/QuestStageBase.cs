using System;
using UnityEngine;

namespace QuestSystem
{
	public abstract class QuestStageBase : ScriptableObject
	{
		public Action OnCompleted;
		protected bool enabled;

		// public virtual void Init(TSetup setup)
		// {
		// 	
		// }

		public void Enable()
		{
			enabled = true;
			Enabled();
		}

		public void Complete()
		{
			Completed();
			OnCompleted?.Invoke();
		}

		protected virtual void Completed() {}
		protected virtual void Enabled() {}
	}
}