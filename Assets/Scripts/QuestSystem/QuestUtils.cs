using System.Collections.Generic;
using System.Linq;

namespace QuestSystem
{
	internal static class QuestUtils
	{
		internal static List<QuestStage> GetNextFor(this Quest obj, QuestStage stage)
		{
			var stageIdx = obj.stages.IndexOf(stage);
			var nextConnections = obj.connections.Where(c => c.outputIdx == stageIdx);
			var nextStages = nextConnections.Select(c => obj.stages[c.inputIdx]);
			return nextStages.ToList();
		}

		internal static List<QuestStage> GetPrevFor(this Quest obj, QuestStage stage)
		{
			var stageIdx = obj.stages.IndexOf(stage);
			var prevConnections = obj.connections.Where(c => c.inputIdx == stageIdx);
			var prevStages = prevConnections.Select(c => obj.stages[c.outputIdx]);
			return prevStages.ToList();
		}

		internal static bool AreNextCompletedFor(this Quest obj, QuestStage stage)
		{
			return obj.GetNextFor(stage).All(q => q.completed);
			// var stageIdx = obj.stages.IndexOf(stage);
			// var nextConnections = obj.connections.Where(x => x.outputIdx == stageIdx);
			// var areCompleted = nextConnections.All(c => obj.stages[c.inputIdx].completed);
			// return areCompleted;
		}

		internal static bool ArePreviousCompletedFor(this Quest obj, QuestStage stage)
		{
			return obj.GetPrevFor(stage).All(q => q.completed);
			// var stageIdx = obj.stages.IndexOf(stage);
			// var prevConnections = obj.connections.Where(x => x.inputIdx == stageIdx);
			// var areCompleted = prevConnections.All(c => obj.stages[c.inputIdx].completed);
			// return areCompleted;
		}
	}
}