using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace QuestSystem
{
	public class Quest : MonoBehaviour
	{
		public List<QuestStageSceneBase> stages;
		public List<Connection> connections;
		
		public int entryIdx;
		
		[Button]
		private void Edit() => Debug.Log("<color=red>editing not implemented :)</color>");

		private void Start()
		{
			foreach (var stage in stages) stage.OnCompleted += OnStageCompleted;
			stages[entryIdx].Enable();
		}
		
		private void OnDestroy()
		{
			foreach (var stage in stages) stage.OnCompleted -= OnStageCompleted;
		}

		private void OnStageCompleted(QuestStageSceneBase stage)
		{
			var next = GetNextFor(stage);
			next.ForEach(CheckIfShouldBeEnabled);
		}

		private void CheckIfShouldBeEnabled(QuestStageSceneBase stage)
		{
			var prevousCompleted = ArePreviousCompletedFor(stage);
			if (prevousCompleted) stage.Enable();
		}

		private List<QuestStageSceneBase> GetNextFor(QuestStageSceneBase stage)
		{
			var stageIdx = stages.IndexOf(stage);
			var nextConnections = connections.Where(c => c.inputIdx == stageIdx);
			var nextStages = nextConnections.Select(c => stages[c.outputIdx]);
			return nextStages.ToList();
		}

		private bool ArePreviousCompletedFor(QuestStageSceneBase stage)
		{
			var stageIdx = stages.IndexOf(stage);
			var prevConnections = connections.Where(x => x.inputIdx == stageIdx);
			var areCompleted = prevConnections.All(c => stages[c.inputIdx].completed);
			return areCompleted;
		}
	}
}