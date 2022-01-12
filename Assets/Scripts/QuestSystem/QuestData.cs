using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
	[CreateAssetMenu(fileName = "NewQuest", menuName = "ScriptableObject/QuestData")]
	public class QuestData : ScriptableObject
	{
		public List<QuestStage> stages = new();
		public List<Connection> connections = new();
		
		[field: SerializeField] private int QuestStageIdx;

		public QuestStage entryStage
		{
			get => stages.Count == 0 ? null : stages[QuestStageIdx];
			set => QuestStageIdx = stages.IndexOf(value);
		}
		
		public void AddEdge(QuestStage @out, QuestStage @in)
		{
			var con = new Connection(stages.IndexOf(@out), stages.IndexOf(@in));
			connections.Add(con);
		}

		public void RemoveEdge(QuestStage @out, QuestStage @in)
		{
			var edge = connections.FirstOrDefault(IsSuitableEdge);
			connections.Remove(edge);

			bool IsSuitableEdge(Connection c) =>
				c.inputIdx == stages.IndexOf(@in) &&
				c.outputIdx == stages.IndexOf(@out);
		}

		public void AddQuestStage(QuestStage q) => stages.Add(q);

		// public List<QuestStage> QuestStagesFor(QuestStage stage)
		// {
		// 	if (!QuestStages.Contains(stage)) throw new ArgumentException("Trying to get next QuestStages for QuestStage not contained within this Quest.");
		// 	var QuestStageIdx = QuestStages.IndexOf(stage);
		// 	var outputConnections = connections.Where(c => c.outputIdx == QuestStageIdx);
		// 	var QuestStages = outputConnections.Select(con => QuestStages[con.inputIdx]);
		// 	return QuestStages.ToList();
		// }
	}
}