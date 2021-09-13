namespace Common.Fsm
{
	public interface IFsm
	{
		void RequestStateChange(IState targetState);
		IState CurrentState { get; }
	}
}