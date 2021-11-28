using Common;

namespace GameInput
{
	public interface IInputSwitchService : IService
	{
		void SetInputFocus(InputFocus target);
		InputFocus current { get; }
	}
}