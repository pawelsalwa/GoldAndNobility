using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
	[Serializable]
	public abstract class QuestStageSceneBase : MonoBehaviour
	{
		public Action<QuestStageSceneBase> OnCompleted;
		protected bool enabled;
		public List<QuestStageSceneBase> next;
		public bool completed { get; private set; } = false;

		/// <summary> Called when prevous quests are finished and this one becomes available </summary>
		internal void Enable()
		{
			enabled = true;
			Enabled();
		}

		public void Complete()
		{
			completed = true;
			Completed();
			OnCompleted?.Invoke(this);
		}

		protected virtual void Completed() { }

		/// <summary> Add custom logic to this stage when it is available to be interacted with </summary>
		protected virtual void Enabled() { }
	}
}