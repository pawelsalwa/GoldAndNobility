namespace Common
{
	public interface IInputSwitchService
	{
		void SetInputFocus(InputFocus target);
		InputFocus current { get; }
	}
}