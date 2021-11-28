using System;

namespace Common
{

	public enum GameStateType { None, MainMenu, Loading, InGame, Paused, InDialogue }

	public static class GameState
	{
		public static event Action<GameStateType> OnChanged;

		private static GameStateType current = GameStateType.None;
		public static GameStateType Current
		{
			get => current;
			set
			{
				current = value;
				OnChanged?.Invoke(value);
			}
		} 
	}
}