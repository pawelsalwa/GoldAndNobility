using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
	[Serializable]
	public abstract class QuestStage : MonoBehaviour
	{
		public Action<QuestStage> OnCompleted;
		public Action<QuestStage> OnEnabled;
		public Action<QuestStage> OnDisabled;
		public Action<QuestStage> OnDestroyed;
		protected bool enabled;
		public List<QuestStage> next;
		[HideInInspector, SerializeField] public Vector2 pos;
		public bool completed { get; private set; } = false;

		/// <summary> Called when prevous quests are finished and this one becomes available </summary>
		internal void Enable()
		{
			enabled = true;
			Enabled();
			OnEnabled?.Invoke(this);
		}
		
		internal void Disable()
		{
			enabled = false;
			Disabled();
			OnDisabled?.Invoke(this);
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

		/// <summary> Called when all of the next stages are completed </summary>
		protected virtual void Disabled() { }

		private void OnDestroy() => OnDestroyed?.Invoke(this);
	}
}