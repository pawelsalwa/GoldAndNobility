using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace QuestSystem
{
	public class Quest : MonoBehaviour
	{
		public List<QuestStage> stages;
		public List<Connection> connections;
		
		public int entryIdx;
		public QuestStage entryStage;

		[Button]
		private void Edit() => Debug.Log("<color=red>editing not implemented :)</color>");

		private void Start()
		{
			foreach (var stage in stages)
			{
				stage.OnCompleted += OnStageCompleted;
				stage.OnDestroyed += OnStageDestroyed;
			}
			stages[entryIdx].Enable();
		}

		private void OnDestroy()
		{
			foreach (var stage in stages)
			{
				stage.OnCompleted -= OnStageCompleted;
				stage.OnDestroyed -= OnStageDestroyed;
			}
		}

		private void OnStageCompleted(QuestStage stage)
		{
			var next = this.GetNextFor(stage);
			next.ForEach(EnableIfPrevCompleted);
			var prev = this.GetPrevFor(stage);
			prev.ForEach(DisableIfNextCompleted);
		}

		private void EnableIfPrevCompleted(QuestStage stage)
		{
			var prevousCompleted = this.ArePreviousCompletedFor(stage);
			if (prevousCompleted) stage.Enable();
		}

		private void DisableIfNextCompleted(QuestStage stage)
		{
			var nextCompleted = this.AreNextCompletedFor(stage);
			if (nextCompleted) stage.Disable();
		}


		public void AddEdge(QuestStage outStage, QuestStage inStage) => connections.Add(new Connection(stages.IndexOf(outStage), stages.IndexOf(inStage)));
		public void RemoveEdge(QuestStage outStage, QuestStage inStage)  => connections.Remove(new Connection(stages.IndexOf(outStage), stages.IndexOf(inStage)));

		public QuestStage AddStage(Type questType)
		{
			if (!questType.IsSubclassOf(typeof(QuestStage))) throw new ArgumentException($"trying to add quest stage of type {questType.Name}, it should derive from {nameof(QuestStage)}");
			var go = new GameObject(questType.Name);
			go.transform.parent = transform;
			var stage = go.AddComponent(questType) as QuestStage;
			stages.Add(stage);
			return stage;
		}

		public void RemoveStage(QuestStage stage)
		{
			if (Application.isPlaying) Destroy(stage.gameObject);
			else DestroyImmediate(stage.gameObject);
		}

		private void OnStageDestroyed(QuestStage stage)
		{
			if (!stage) return;
			stage.OnDestroyed -= OnStageDestroyed;
			stage.OnCompleted -= OnStageCompleted;
			stages.Remove(stage);
		}
	}
}