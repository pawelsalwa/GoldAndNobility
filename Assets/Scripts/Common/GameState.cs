namespace Common
{

	public enum GameStateType { None, MainMenu, Loading, InGame, Paused, InDialogue }

	public static class GameState
	{
		public static GameStateType Current = GameStateType.None;
	}
}