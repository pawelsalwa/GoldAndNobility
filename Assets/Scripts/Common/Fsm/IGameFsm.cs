
namespace Common.Fsm
{
	public interface IGameFsm
	{
		void StartApplication();
		void EnterMainMenu();
		void StartNewGame();
		void EnterLoading();
		void FinishLoading();
		void EnterGame();
		void PauseGame();
		void UnpauseGame();
	}
}