namespace UI
{
	public interface IUIPanel
	{
		bool Active { get; }
		void Open();
		void Close();
	}
}