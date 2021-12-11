using System;
using System.Collections.Generic;

namespace Common
{

	public enum GameStateType { None, MainMenu, Loading, InGame, BrowsingInventory, Paused, InDialogue, Trading, AnnouncementTableBrowsing }

	public static class GameState
	{
		public static event Action<GameStateType> OnChanged;

		private static readonly List<GameStateType> statesQueue = new() {GameStateType.None}; // Inited with 'None' to prevent out of range exceptions

		public static GameStateType Current => statesQueue[^1];

		public static void ChangeState(GameStateType state)
		{
			if (statesQueue.Contains(state))
				statesQueue.Remove(state);
			statesQueue.Add(state);
			Notify();
		}

		/// <summary> Cancels game state to previous one </summary>
		/// <param name="state"> state set previously to be cancelled </param>
		public static void CancelState(GameStateType state)
		{
			if (!statesQueue.Contains(state)) return;// throw new ArgumentException("Cant cancel state that hasn't been set previously!");
			statesQueue.Remove(state);
			Notify();
		}
		
		private static void Notify() => OnChanged?.Invoke(statesQueue[statesQueue.Count - 1]);
	}
}