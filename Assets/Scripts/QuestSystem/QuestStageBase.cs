using System;
using UnityEngine;

namespace QuestSystem
{
	[CreateAssetMenu(menuName = "Create QuestStageObject", fileName = "QuestStageObject", order = 0)]
	public class QuestStageBase : ScriptableObject
	{
		public Action OnCompleted;

		public void Complete()
		{
			OnCompleted?.Invoke();
		}
	}
}