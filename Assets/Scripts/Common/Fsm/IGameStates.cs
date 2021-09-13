
namespace Common.Fsm
{
	public interface IGameStates
	{
		IState Current { get; }
		IState StartingApp { get; }
		IState MainMenu { get; }
		IState Loading { get; }
		IState LoadedAndWaiting { get; }
		IState InGame { get; }
		IState GamePaused { get; }
	}
}