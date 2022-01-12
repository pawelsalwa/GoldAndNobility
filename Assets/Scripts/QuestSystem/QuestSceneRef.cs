using System;
using InventorySystem;
using TradeSystem;
using UnityEngine;

namespace QuestSystem
{
	public class QuestSceneRef : MonoBehaviour
	{

		public QuestData questData;
		public TradeEntity playerTradeEntity;
		public ItemData itemDataForQuestFinish;
		public int itemCountForQuestFinish;

		private void Start()
		{
			playerTradeEntity.inventory.OnChangedAt += OnInventoryChangedAt;
		}

		private void OnInventoryChangedAt(int idx, ItemStack item)
		{
			if (item.data != itemDataForQuestFinish) return;
			if (item.count < itemCountForQuestFinish) return;
			Debug.Log("<color=white>quest finished</color>");
		}

	}
}