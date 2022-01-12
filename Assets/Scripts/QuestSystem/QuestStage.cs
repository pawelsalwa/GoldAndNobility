using System;
using UnityEngine;

namespace QuestSystem
{
	[Serializable]
	public class QuestStage
	{
		public event Action OnCompleted;
		public bool completed = false;
		public string text = "Quest stage";
		public Rect pos;
		public QuestStageBase questStageBase;

		public void MarkCompleted()
		{
			completed = true;
			OnCompleted?.Invoke();
		}
	}
}